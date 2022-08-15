using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class elevator3_3 : MonoBehaviour
{
    [SerializeField] GetComponent getcom;
    [SerializeField] PhotonView view;
    [SerializeField] int RPC_playercount;
    [SerializeField] GameObject Boss;
    [SerializeField]
    GameObject DelectThis;
    [SerializeField] Text text;
    bool trigger;

    [SerializeField] Animator animator;

    [SerializeField] Spike_Spawn_Timmer[] spike_;
    [SerializeField] GameObject objForSpike;
    private void Start()
    {
        Invoke(nameof(Dealy), .2f);
    }
    void Dealy()
    {
        text.text = GameObject.FindGameObjectsWithTag("PlayerTrigger").Length.ToString() + "/" + RPC_playercount.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!trigger)
        {
            if (collision.tag == "Player" && collision.gameObject == getcom.Playerobject)
            {
                trigger = true;
                view.RPC("ReFrash", RpcTarget.All, true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject == getcom.Playerobject)
        {
            if (trigger)
            {
                view.RPC("ReFrash", RpcTarget.All, false);
                trigger = false;
            }
        }
    }

    void TriggerELEV()
    {
        Boss.SetActive(true);
        foreach(Spike_Spawn_Timmer st in spike_)
        {
            st.GameOBJ = objForSpike;
        }
        Destroy(DelectThis);
    }
    [PunRPC]
    void ReFrash(bool YorN)
    {
        if (YorN)
        {
            RPC_playercount++;
        }
        else if(!YorN)
        {
            RPC_playercount--;
        }
        if(RPC_playercount == GameObject.FindGameObjectsWithTag("Player").Length)
        {
            animator.SetTrigger("Trigger");
            Invoke(nameof(TriggerELEV), 5.58f);
        }
        text.text = GameObject.FindGameObjectsWithTag("Player").Length.ToString() + "/" + RPC_playercount.ToString();
    }
}
