using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulSelect : MonoBehaviour
{
    public int pnum;
    [SerializeField] GetComponent getcomp;
    playermovement pm;
    public void player01()
    {
        PlayerPrefs.SetInt("P1", pnum);
        if(pnum == PlayerPrefs.GetInt("P2")) { PlayerPrefs.SetInt("P2", 1); }

        Debug.Log("玩家一:" + PlayerPrefs.GetInt("P1") + "玩家二:" + PlayerPrefs.GetInt("P2"));
        Refrash(1);
    }

    public void player02()
    {
        PlayerPrefs.SetInt("P2", pnum);
        if (pnum == PlayerPrefs.GetInt("P1")) { PlayerPrefs.SetInt("P1", 1); }


        Debug.Log("玩家一:" + PlayerPrefs.GetInt("P1") + "玩家二:" + PlayerPrefs.GetInt("P2"));
        Refrash(2);
    }


    public void playerReSet()
    {
        PlayerPrefs.SetInt("P2", 0);
        PlayerPrefs.SetInt("P1", 0);
        Debug.Log("玩家一:" + PlayerPrefs.GetInt("P1") + "玩家二:" + PlayerPrefs.GetInt("P2"));
        Refrash(0);
    }

    void Refrash(int state0or2)
    {
        pm = getcomp.Playerobject.GetComponent<playermovement>();
        

        if (state0or2 == 1) 
        {
            pm.switching = true;
            pm.Filter();
            pm.switching = false;
        }
        else if (state0or2 == 2)
        {
            pm.switching = false;
            pm.Filter();
            pm.switching = true;
        }
        else if (state0or2 == 0)
        {
            pm.switching = !pm.switching;
            pm.Filter();
            pm.switching = !pm.switching;
        }
        pm.SwitchEffectCall();
        pm.nextswitch = Time.time + 1;
        
    }
}
