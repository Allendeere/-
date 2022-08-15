using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUpdate : MonoBehaviour
{
    [SerializeField] Mission mission;
    [Header("任務名稱 / 該名稱的進度")]
    [SerializeField] string 任務代號;
    [SerializeField] int 任務進度;

    [Header("任務測試")]
    [SerializeField] bool testfor任務;
    [SerializeField] GameObject 任務事件Plant;
    private void Start()
    {
        mission.SideQuests = PlayerPrefs.GetString("mission");
        if (testfor任務)
        {
            if (!mission.SideQuests.Contains(任務代號 + 任務進度.ToString()))
            {
                任務事件Plant.SetActive(true);
            }
        }
    }
    public void missionupdate(int num) // 0 = False / 1 = True / 套用於NPC聊天的按鈕
    {
        if(!mission.SideQuests.Contains(任務代號 + 任務進度.ToString()))
        {
            mission.SideQuests += "@" + 任務代號 + 任務進度.ToString() + ":" + num.ToString() ;
            PlayerPrefs.SetString("mission", mission.SideQuests);
        }
        else
        {

        }
        //mission.SideQuests = mission.SideQuests.Insert(第n項任務, num.ToString());
    }

}
