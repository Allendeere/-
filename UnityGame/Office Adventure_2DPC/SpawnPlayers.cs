using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayers : MonoBehaviour
{
    [Header("³s ±µ : GameObjectManager")]

    public GameObject Player;
    public static SpawnPlayers s;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] PhotonView view;
    [SerializeField] GameObject[] playerobj;
    ReLoad reLoad;

    void Start()
    {
         reLoad = GameObject.Find("ReLoad").GetComponent<ReLoad>();
        if (!reLoad.V_)
            return;
            s = this;
       Vector2 spawn = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
       GameObject myplayer =  PhotonNetwork.Instantiate(Player.name, spawn, Quaternion.identity);


        view.RPC("Set", RpcTarget.All);
    }


    public static void WhenDestory()
    {
        //s.Invoke(nameof(ForTheDelay), .5f);
        //s.ForTheDelay();
        s.view.RPC("Set", RpcTarget.All);
    }
    
    void ForTheDelay()
    {



    }

    [PunRPC]
    void Set()
    {
        GameObjectManager.instance.allObjects.Clear();
       playerobj = GameObject.FindGameObjectsWithTag("Player");
        if (playerobj.Length > 0)
        {
            GameObjectManager.instance.allObjects.AddRange(playerobj);
        }
        
    }



}
