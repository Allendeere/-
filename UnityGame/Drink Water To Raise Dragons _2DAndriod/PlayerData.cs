using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int EXP_Level;
    public int EXP_Experience;

    public bool Setting_Vibrate;
    public bool Setting_Notifications;

    public int min, hour;
    public bool missionLock;
    public int totalmission;

    public PlayerData(EXP exp,Mission mission,Setting setting)
    {
        EXP_Level = exp.Level;
        EXP_Experience = exp.Experience;
        min = mission.min;
        hour = mission.hour;
        totalmission = mission.totalmission;
        missionLock = mission.missionLock;
        Setting_Notifications = setting.IsNotifications;
        Setting_Vibrate = setting.IsVibration;
    }
}
