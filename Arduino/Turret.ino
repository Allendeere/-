#include <SoftwareSerial.h>
#include <DFRobotDFPlayerMini.h>
#include <ADCTouch.h>
#include <Servo.h>

#define SIGNAL_PIN 5
#include <Enerlib.h>

SoftwareSerial DFPlayerSerial(3, 2);
DFRobotDFPlayerMini myDFPlayer;

Servo myservo;  // X軸
Servo myservo2; // 雙臂
Energy energy;

volatile byte state = 0;

int State_;
int ref0, ref1;
bool Friendly_;
int Timmer_;
bool TargetLost_;
int pos = 0;// X軸
int pos2 = 0;// 雙臂
void EnergyState()
{
  if(energy.WasSleeping())
  {state = 1;}
  else
  {state = 2;}
  
}
void setup()
{
  DFPlayerSerial.begin(9600);
  Serial.begin(9600);

  attachInterrupt(0, EnergyState,CHANGE);

  Serial.println();
  Serial.println(F("DFRobot DFPlayer Mini Demo"));
  Serial.println(F("Initializing DFPlayer ... (May take 3~5 seconds)"));
  
  if(myDFPlayer.begin(DFPlayerSerial))
  {    Serial.println("連接成功>w<");  }
  else
  {    Serial.println("連接失敗XnX");  }


  myDFPlayer.begin(DFPlayerSerial);
  myDFPlayer.volume(25);

  pinMode(SIGNAL_PIN, INPUT);
  pinMode(7, INPUT);
  pinMode(8, INPUT);
  myservo.attach(9);
  myservo2.attach(6);
  ref0 = ADCTouch.read(A0, 500);
  ref1 = ADCTouch.read(A1, 500);
  pos = 90;
  pos2 = 0;

  TargetLost_ = false;
  Friendly_ = true;
  Timmer_ = 1000;
  State(2);
  pos2 = 90;
  myservo2.write(pos2);
  pos = 90;
  myservo.write(pos);
  myDFPlayer.play(2);
  
}
void loop() {
  
  if (state == 1) 
  {Serial.println("【 休 眠 】");}
  else if (state == 2) 
  {Serial.println("【 解 除 休 眠 】");}
  state = 0;
  
  //Serial.println(Friendly_); // 可 註 記。。。

  if(digitalRead(SIGNAL_PIN)==HIGH&& !TargetLost_){
    if(State_ == 1)
    {State(2);delay(3000);}
    else if (State_ ==2)
    {Serial.println(" 激 活 中。。。"  );State(3);}
  }

  if(State_ == 1){
    energy.PowerDown();
    if(pos2!=0){
    pos2 = 0;
    myservo2.write(pos2);
    }
    if(pos!=90){
    pos = 90;
    myservo.write(pos);
    }
  }
  
  if(State_ >= 2){
    
    int value0 = ADCTouch.read(A0);
    int value1 = ADCTouch.read(A1);

    value0 -= ref0;
    value1 -= ref1;

    if (value0 > 40 && value1 > 40)
    {State(5);}
    else if (value0 > 40 && value1 < 40)
    {State(1);}
    else if (value0 < 60 && value1 > 60)
    {
      Friendly_ = !Friendly_;
      myDFPlayer.play(6);
    }
    
  }
  if(State_ ==2&& TargetLost_){
  delay(9000);
  TargetLost_ = false;
  }
  
  if(State_ == 3){
     TragetFound();
  }
  if(State_ == 4){
     TargetLost();
  }
  if(State_ == 5){
      Sing();
  }
  delay(Timmer_);
}

void State(int state){
  
  switch (state) {
        case 1:
          Serial.println(F(" 關 機 :【1】"));
          Timmer_ = 1000;
          if(Friendly_){myDFPlayer.play(7);}
          else{myDFPlayer.play(1);}
          if(pos2!=0)
          {
          pos2 = 0;
          myservo2.write(pos2);
          }
          if(pos!=90)
          {
          pos = 90;
          myservo.write(pos);
          }
          energy.PowerDown();
          delay(9000);
          break;
        case 2:
          Serial.println(F(" 待 機 模 式 :【2】"));
          myDFPlayer.volume(20);
          Timmer_ = 1000;
          //pos2 = 90;
          //myservo2.write(pos2);
          break;
        case 3:
          Serial.println(F(" 獲 得 目 標 :【3】"));
          Timmer_ = 15;
          if(Friendly_){myDFPlayer.play(8);}
          else{myDFPlayer.play(3);}
          
          break;
        case 4:
          Serial.println(F(" 失 去 目 標 :【4】"));
          //if(Friendly_){myDFPlayer.play(9);}
          //else{myDFPlayer.play(4);}
          break;
        case 5:
          Serial.println(F(" 歡 送 模 式 :【5】"));
          Timmer_ = 1000;
          myDFPlayer.play(5);
          break;
  }

  State_ = state;
}

void TragetFound(){
  
  
  if(digitalRead(SIGNAL_PIN)==HIGH ){
    if(pos2!=90){
      pos2 = 90;
    myservo2.write(pos2);
    }
    /*if(Timmer_ != 10){
    Timmer_ = 10;  
    }*/
    if(digitalRead(7)==HIGH && digitalRead(8)==HIGH)
   {
     Timmer_ = 1000;  
   }
   else if(digitalRead(7)==LOW && digitalRead(8)==HIGH)
   {
    if(Timmer_ != 10){
    Timmer_ = 10;  
    }
    pos = pos + 5;
      myservo.write(pos);
   }
    else if(digitalRead(7)==HIGH && digitalRead(8)==LOW)
   {
    if(Timmer_ != 10){
    Timmer_ = 10;  
    }
    pos = pos - 5;
    myservo.write(pos);
   }
  }
  else{
    
    if  (Friendly_){myDFPlayer.play(9);}
    else  {myDFPlayer.play(10);}
    
    
    for (pos = 90; pos <= 180; pos += 1) { 
    myservo.write(pos);
    delay(15);  
    }
    for (pos = 180; pos >= 0; pos -= 1) {
    myservo.write(pos);              
    delay(15);                      
    }
    for (pos = 0; pos <= 90; pos += 1) { 
    myservo.write(pos);
    delay(15);  
    }
    State(4);
   /* Timmer_++;  Serial.println(F("計時器 + + "));
    if(Timmer_>1)
    {
      State(4);
      Timmer_ = 0;  

    }*/
    
    
  }
}

void TargetLost(){
      if  (Friendly_){myDFPlayer.play(11);}
      else  {myDFPlayer.play(4);}
      TargetLost_ = true;
      State(2);
       //Servo 0~90 90~0
  /*if(digitalRead(SIGNAL_PIN)==HIGH )
  {
    State(3);
  }
  else
  {    
  Timmer_++;
  if(Timmer_>3)
    {
      State(2);
      Timmer_ = 0;  
    }
  delay(1000);
  }*/
}

void Sing(){
  Timmer_ = 999999;
  myDFPlayer.volume(28);
  delay(162000);
  Timmer_ = 1000;
}
