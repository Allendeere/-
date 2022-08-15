using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;
using System;

public class Data : MonoBehaviour
{
    [SerializeField]  PlayerData data;
    [SerializeField] Setting setting;
    [SerializeField] Mission mission;
    [SerializeField] EXP exp;

    private void Awake()
    {
        //   data = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("jsondata"));
        PlayerData data = SaveLoad.LoadPlayer();

        setting.IsNotifications = data.Setting_Notifications;
        setting.IsVibration = data.Setting_Vibrate;

        exp.Level = data.EXP_Level;
        exp.Experience = data.EXP_Experience;

        mission.min = data.min;
        mission.hour = data.hour;
        mission.missionLock = data.missionLock;
    }

    public void SavePlayer()
    {
        SaveLoad.SaveSystem(exp, mission, setting);
    }
}
