using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LVcomplete : MonoBehaviour
{
    public int levelload = 0;
    //public GameObject player;
    //public GameObject lightobj;
    
    public int levelToUnlock = 2;
    public Animator objanimator;
    [SerializeField] BoxCollider2D edgeCollider2;

    bool YesItHasPlayerOnIt;
    public Animator UIanimator;
    public GameObject UIplant;
    [SerializeField] PhotonView view;
    private int getlv;
    [SerializeField] Conscience conscience;

       
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
                YesItHasPlayerOnIt = true;
            }
    }

    public void TestForPlayerOnIt()
    {
        if(YesItHasPlayerOnIt == true)
        {
            view.RPC("RPC_trigger", RpcTarget.All);
            edgeCollider2.enabled = false;
            YesItHasPlayerOnIt = false;
        }
    }


    void delay()
    {
        ReLoad reLoad = GameObject.Find("ReLoad").GetComponent<ReLoad>();
        //reLoad.transform_ = new Vector2(-5, -2);
        getlv = PlayerPrefs.GetInt("levelReached");
        if (getlv < levelToUnlock) { PlayerPrefs.SetInt("levelReached", levelToUnlock); }
 
        Debug.Log("delay");

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel("menu");
        }

    }
   
    void trigger_ ()
    {
        Debug.Log("trigger_");
        /*
        GameObject[] allPlayer = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < allPlayer.Length; i++)
        {
            if(allPlayer != null)
            {
                Destroy(allPlayer[i]);

            }
        
        }*/

        conscience.LevelComplete();


        FindObjectOfType<AudioManger>().Play("LevelComplet");
        Invoke(nameof(delay), 1);

        //if (lightobj != null) { lightobj.SetActive(true); }
        //if (player != null) { player.SetActive(false); }
        if (objanimator != null) { objanimator.SetBool("in", true); }
        edgeCollider2.enabled = false;

        if (UIplant != null)
        {
            UIplant.SetActive(true);
            if (UIanimator != null)
            {
                UIanimator.SetBool("trigger", true);
            }
        }
    }

    [PunRPC]
    void RPC_trigger()
    {
        trigger_(); 
    }


}