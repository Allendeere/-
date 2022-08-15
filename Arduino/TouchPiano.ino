#include <SoftwareSerial.h>
#include <ADCTouch.h>
#include <DFRobotDFPlayerMini.h>
#include <Enerlib.h>

Energy energy;
SoftwareSerial DFPlayerSerial(3, 2);
DFRobotDFPlayerMini myDFPlayer;

int ref0, ref1, ref2, ref3, ref4, ref5;
int num = 500;
volatile byte state = 0;
bool trigger = false;

void EnergyState()
{
  if(energy.WasSleeping())
  {state = 1;}
  else
  {state = 2;}
  
}
void setup() {
    DFPlayerSerial.begin(9600);
    Serial.begin(9600);

    attachInterrupt(0, EnergyState,CHANGE);

    ref0 = ADCTouch.read(A0, 500);
    ref1 = ADCTouch.read(A1, 500);
    ref2 = ADCTouch.read(A2, 500);
    ref3 = ADCTouch.read(A3, 500);
    
    ref4 = ADCTouch.read(A4, 500);
    ref5 = ADCTouch.read(A5, 500);
    
    if(myDFPlayer.begin(DFPlayerSerial))
    {    Serial.println("DFPlayer連接成功");  }
    else
    {    Serial.println("DFPlayer連接失敗");  }

    myDFPlayer.begin(DFPlayerSerial);
    myDFPlayer.volume(25);

    myDFPlayer.play(2);

}

void loop() {
    if (state == 1) 
    {Serial.println("【 休 眠 】");          num = 9000;}
    else if (state == 2) 
    {Serial.println("【 解 除 休 眠 】");    num = 500;}
    state = 0;
    int A0 = ADCTouch.read(A0);
    int A1 = ADCTouch.read(A1);
    int A2 = ADCTouch.read(A2);
    int A3 = ADCTouch.read(A3);
    int A4 = ADCTouch.read(A4);//。
    int A5 = ADCTouch.read(A5);

    A0 -= ref0;
    A1 -= ref1;
    A2 -= ref2;
    A3 -= ref3;
    A4 -= ref4;//。
    A5 -= ref5;

    if(trigger == true){
    if(A0 > 60&&(A1 < 60)&&(A2 < 60)&&(A3 < 60))
    {
      if((A4 > 60)&&(A5 < 60)){Serial.print(" + A0");  myDFPlayer.play(1);}
      if((A4 < 60)&&(A5 < 60)){Serial.print("   A0");  myDFPlayer.play(5);}
      if((A4 < 60)&&(A5 > 60)){Serial.print(" - A0");  myDFPlayer.play(9);}
      }


    if(A0 < 60&&(A1 > 60)&&(A2 < 60)&&(A3 < 60))
    {
      if((A4 > 60)&&(A5 < 60)){Serial.print(" + A1");  myDFPlayer.play(2);}
      if((A4 < 60)&&(A5 < 60)){Serial.print("   A1");  myDFPlayer.play(6);}
      if((A4 < 60)&&(A5 > 60)){Serial.print(" - A1");  myDFPlayer.play(10);}
      }

      
    if(A0 < 60&&(A1 < 60)&&(A2 > 60)&&(A3 < 60))
    {
      if((A4 > 60)&&(A5 < 60)){Serial.print(" + A2");  myDFPlayer.play(3);}
      if((A4 < 60)&&(A5 < 60)){Serial.print("   A2");  myDFPlayer.play(7);}
      if((A4 < 60)&&(A5 > 60)){Serial.print(" - A2");  myDFPlayer.play(11);}
      }

    if(A0 < 60&&(A1 < 60)&&(A2 < 60)&&(A3 > 60))
    {
      if((A4 > 60)&&(A5 < 60)){Serial.print(" + A3");  myDFPlayer.play(4);}
      if((A4 < 60)&&(A5 < 60)){Serial.print("   A3");  myDFPlayer.play(8);}
      if((A4 < 60)&&(A5 > 60)){Serial.print(" - A3");  myDFPlayer.play(12);}
    }
    }

    if((A0 > 60)&&(A1 > 60)&&(A2 > 60)&&(A3 > 60)&&(A4 < 60)&&(A5 < 60)){ num = 9000;  trigger = !trigger;}// 關 機
    if(trigger == false){
      energy.PowerDown();
    }

    delay(num);
    /* 。 1 2 3 4
    if((A0 > 60)&&(A1 < 60)&&(A2 < 60)&&(A3 < 60)&&(A4 < 60)&&(A5 < 60)){}
    if((A0 < 60)&&(A1 > 60)&&(A2 < 60)&&(A3 < 60)&&(A4 < 60)&&(A5 < 60)){}
    if((A0 < 60)&&(A1 < 60)&&(A2 > 60)&&(A3 < 60)&&(A4 < 60)&&(A5 < 60)){}
    if((A0 < 60)&&(A1 < 60)&&(A2 < 60)&&(A3 > 60)&&(A4 < 60)&&(A5 < 60)){}

    // 。 5 6 7 8
    if((A0 > 60)&&(A1 < 60)&&(A2 < 60)&&(A3 < 60)&&(A4 > 60)&&(A5 < 60)){}
    if((A0 < 60)&&(A1 > 60)&&(A2 < 60)&&(A3 < 60)&&(A4 > 60)&&(A5 < 60)){}
    if((A0 < 60)&&(A1 < 60)&&(A2 > 60)&&(A3 < 60)&&(A4 > 60)&&(A5 < 60)){}
    if((A0 < 60)&&(A1 < 60)&&(A2 < 60)&&(A3 > 60)&&(A4 > 60)&&(A5 < 60)){}

    // 。 -3 -2 -1 0
    if((A0 > 60)&&(A1 < 60)&&(A2 < 60)&&(A3 < 60)&&(A4 < 60)&&(A5 > 60)){}
    if((A0 < 60)&&(A1 > 60)&&(A2 < 60)&&(A3 < 60)&&(A4 < 60)&&(A5 > 60)){}
    if((A0 < 60)&&(A1 < 60)&&(A2 > 60)&&(A3 < 60)&&(A4 < 60)&&(A5 > 60)){}
    if((A0 < 60)&&(A1 < 60)&&(A2 < 60)&&(A3 > 60)&&(A4 < 60)&&(A5 > 60)){}


    
    if((A0 < 60)&&(A1 < 60)&&(A2 < 60)&&(A3 < 60)&&(A4 < 60)&&(A5 < 60)){}
    if((A0 < 60)&&(A1 < 60)&&(A2 < 60)&&(A3 < 60)&&(A4 < 60)&&(A5 < 60)){}
    if((A0 < 60)&&(A1 < 60)&&(A2 < 60)&&(A3 < 60)&&(A4 < 60)&&(A5 < 60)){}
    if((A0 < 60)&&(A1 < 60)&&(A2 < 60)&&(A3 < 60)&&(A4 < 60)&&(A5 < 60)){}

    */
}
