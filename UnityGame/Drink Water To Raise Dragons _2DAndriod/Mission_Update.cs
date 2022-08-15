using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission_Update : MonoBehaviour
{
    string oldSeconds;

    int minutesInt;
    int hoursInt;

    //int NextMission_min;
    //int NextMission_hour;
    [SerializeField] GameObject MissionPlant;

    [SerializeField] Text mission_timer;
    [SerializeField] Text mission_total;

    [SerializeField] Mission mission;
    [SerializeField] Setting setting;

    private void Start()
    {
        mission_timer.text = mission.hour + ":" + mission.min;
        mission_total.text = mission.totalmission + "";
    }
    private void Update()
    {
        string seconds = System.DateTime.UtcNow.ToString("ss");

        if(seconds != oldSeconds)
        {
            UpdateTimer();
        }
        oldSeconds = seconds;


        if (hoursInt >= mission.hour && minutesInt >= mission.min && mission.missionLock == false)
        {

            if (setting.IsVibration)
            {
                Vibration.Vibrate(1000);
            }
            mission.missionLock = true;
        }
        UpdateMission();
    }

    void UpdateMission()
    {
       

        if (mission.missionLock == true)
        {
            MissionPlant.SetActive(true); 
        }

    }
    void UpdateTimer()
    {
         minutesInt = int.Parse(System.DateTime.UtcNow.ToString("mm"));
         hoursInt = int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("hh"));

        //print(hoursInt + ":" + minutesInt + ":" + secondsInt);
    }

    public void Mission_CoundDown()
    {
        mission.totalmission++;
        if (setting.IsNotifications)
        {Notifications.SendMessage();}

        mission.missionLock = false;

        mission.min = minutesInt;
        mission.hour = hoursInt;
        mission.min = mission.min + 50;
        mission.hour++;
        if (mission.min > 60) { mission.min = mission.min - 60; mission.hour++; }
        if (mission.hour > 24) {mission.hour = mission.hour - 12; }
        mission_timer.text = mission.hour + ":" + mission.min;
        mission_total.text = mission.totalmission + "";
    }

}
