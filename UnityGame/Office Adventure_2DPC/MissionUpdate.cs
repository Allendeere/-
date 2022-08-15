using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUpdate : MonoBehaviour
{
    [SerializeField] Mission mission;
    [Header("���ȦW�� / �ӦW�٪��i��")]
    [SerializeField] string ���ȥN��;
    [SerializeField] int ���ȶi��;

    [Header("���ȴ���")]
    [SerializeField] bool testfor����;
    [SerializeField] GameObject ���Ȩƥ�Plant;
    private void Start()
    {
        mission.SideQuests = PlayerPrefs.GetString("mission");
        if (testfor����)
        {
            if (!mission.SideQuests.Contains(���ȥN�� + ���ȶi��.ToString()))
            {
                ���Ȩƥ�Plant.SetActive(true);
            }
        }
    }
    public void missionupdate(int num) // 0 = False / 1 = True / �M�Ω�NPC��Ѫ����s
    {
        if(!mission.SideQuests.Contains(���ȥN�� + ���ȶi��.ToString()))
        {
            mission.SideQuests += "@" + ���ȥN�� + ���ȶi��.ToString() + ":" + num.ToString() ;
            PlayerPrefs.SetString("mission", mission.SideQuests);
        }
        else
        {

        }
        //mission.SideQuests = mission.SideQuests.Insert(��n������, num.ToString());
    }

}
