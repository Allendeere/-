using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] PhotonView view;
    private GameMaster gm;
    public Animator animator;
    public Collider2D hitbox;
    public bool sound;
    [SerializeField] bool KeepUpdate;
    bool Updating;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                view.RPC("Chack", RpcTarget.All);
            }

            
        }
    }
    private void Update()
    {
        if (Updating) { 
            gm.lastCheckPointPos = transform.position + new Vector3(0, 1, 0);
        }
    }
    [PunRPC]
    void Chack()
    {
        if (KeepUpdate)
        {
            Updating = true;
        }
        else
        {
            Vector3 posadd = new Vector3(0, 1, 0);
            gm.lastCheckPointPos = transform.position + posadd;
        }

        hitbox.enabled = false;
        if (animator != null) { animator.SetBool("starttouch", true); }
        if (sound == true) { FindObjectOfType<AudioManger>().Play("checkpoint"); }
    }
}
