using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class tracking : MonoBehaviour
{
    [SerializeField] PhotonView view;
    [SerializeField] GameObjectManager GameObjectManager;
    [SerializeField] GameObject gobj;
    [SerializeField] float MoveSpeed;
    [SerializeField] int LifeTime = 4;
    Vector2 direction = new Vector2();

    private void Start()
    {
        GameObjectManager = GameObject.Find("SpawnPlayers").GetComponent<GameObjectManager>();
        Mester_start();
        Invoke(nameof(Kill), LifeTime);
    }
    void Kill()
    {
        Destroy(this.gameObject);
    }
    void Mester_start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (GameObjectManager.allObjects.Count == 0)// ¥¼ ÀË ´ú ¨ì ¤H
            {
                Kill();
            }
            else
            {
                gobj = GameObjectManager.allObjects[Random.Range(0, GameObjectManager.allObjects.Count)];
                int PlayerID = gobj.GetComponent<PhotonView>().ViewID;
                
                view.RPC("GetPlayer_multiple", RpcTarget.All, PlayerID);
            }

        }
    }
    [PunRPC]
    public void GetPlayer_multiple(int id)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            foreach(GameObject enemy in GameObjectManager.allObjects)
            {
                if(id == enemy.GetComponent<PhotonView>().ViewID)
                {
                    gobj = enemy;
                }
            }
        }
    }

    private void Update()
    {
        if (gobj == null)
            return;
        if (PhotonNetwork.IsMasterClient)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, gobj.transform.position, MoveSpeed * Time.deltaTime);



            direction = gobj.transform.position - transform.position;
            Vector3 v3 = direction.x < 0 ?
    new Vector3(0, 0, 0) : new Vector3(0, -180, 0);

            transform.eulerAngles = direction.x < 0 ?
                Vector3.MoveTowards(this.transform.eulerAngles, v3, 500) :
                Vector3.MoveTowards(this.transform.eulerAngles, v3, 500);
        }
    }
}
