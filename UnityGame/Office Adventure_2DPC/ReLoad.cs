using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class ReLoad : MonoBehaviour
{

    public Vector2 transform_;
    public bool V_;
    public string enemyTag = "Player";
    private void Awake()
    {
        DontDestroyOnLoad(this);

        //。如果不存在此名稱的 Int，則默認為 0。
        DataSetup.Mission_Progress = PlayerPrefs.GetInt("Mission_Progress", 0);
    }

    public void F()
    {
        //Invoke(nameof(ff), 2);
        Invoke(nameof(ff), .2f);
    }

    void ff()
    {
        M_Vote mv = GameObject.Find("LevelLoad").GetComponent<M_Vote>();
        mv.level = 1;
        mv.Skip_();
    }

    public void leaveroom()
    {
        transform_ =  new Vector2(0,0);
        PhotonNetwork.LeaveRoom();
        Invoke(nameof(_leaveroom), 0.2f);
    }

    void _leaveroom()
    {
        PhotonNetwork.LoadLevel("Loading");

        V_ = false;
    }


}

public static class DataSetup //(1)
{
    [Header("Mission_Progress")]
    public static int Mission_Progress;

    public static void MissionCheak()
    {
        Debug.Log("ReLoad - MissionCheak" + Mission_Progress);
        if(Mission_Progress ==0){ LevelSelecter.RunMission(0); }//(2)
        else if (Mission_Progress == 1) {  }
        else if (Mission_Progress == 2) { LevelSelecter.RunMission(1); }
        // . . . 有些為空 如4 = null
    }
}