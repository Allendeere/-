using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class cp : MonoBehaviour
{
    [SerializeField] Image CP_tracking;
    [SerializeField] Image CP_trackingII;
    [SerializeField] Slider CPSlider;
    [SerializeField] GameObject CPUI;
    Vector3 cppoint= new Vector3(0,1.4f,0);
    [SerializeField] GetComponent getComponent;
    GameObject player;


    [SerializeField] int CatchPointTime;
    [SerializeField] PhotonView view;
    [SerializeField] bool onPoint;
    [SerializeField] float leave;
    [SerializeField] SpriteRenderer point_image;
    [SerializeField] Sprite point_image_on;
    float tick;
    bool Cap;

    [SerializeField] GameObject CPcap;
    [SerializeField] GameObject CPUIcap;

    [Header("成功佔點額外觸發")]
    [SerializeField] GameObject OBJ隱藏;
    [SerializeField] GameObject OBJ顯示;
    [SerializeField] Animator OBJ動畫Trigger;
    private void Start()
    {
        Invoke(nameof(Delay), .5f);
    }

    void Delay()
    {
        if (getComponent.Playerobject != null)
        {
            player = getComponent.Playerobject;
        }
        else
        {
            Invoke(nameof(Delay), .5f);
        }
    }
    private void Update()
    {
        
        //======================================================================================= U I Tracking
        if (CatchPointTime < 40)
        {
            CPSlider.value = CatchPointTime;
            if (player != null)
            {
                var cpcolor = CP_tracking.color;
                var cpcolor2 = CP_trackingII.color;
                float dist = Vector3.Distance(transform.position, player.transform.position);

                if (dist <= 6)
                {
                    cpcolor.a = 1;
                    cpcolor2.a = 1;
                    CPUI.transform.localScale = Vector3.MoveTowards(CPUI.transform.localScale, new Vector3(.4f, .4f, 1), 3);
                    CPSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + cppoint);
                    CPSlider.transform.eulerAngles = Vector3.MoveTowards(CPSlider.transform.eulerAngles, new Vector3(0, 0, 90), 15);
                }
                else if (dist > 6)
                {

                    CPUI.transform.localScale = Vector3.MoveTowards(CPUI.transform.localScale, new Vector3(.25f, .25f, 1),2);
                    CPSlider.transform.eulerAngles = transform.position.x > player.transform.position.x ?
                        Vector3.MoveTowards(CPSlider.transform.eulerAngles, new Vector3(0, 0, 180), 15) :
                       Vector3.MoveTowards(CPSlider.transform.eulerAngles, new Vector3(0, 0, 0), 15);

                    Vector3 outofscreen = transform.position.x > player.transform.position.x ?
                        new Vector3(Camera.main.transform.position.x + 6, transform.position.y + .5f, 0) :
                        new Vector3(Camera.main.transform.position.x - 6, transform.position.y + .5f, 0);

                    CPSlider.transform.position = Camera.main.WorldToScreenPoint(outofscreen);

                    if (dist < 10)
                    {
                        cpcolor.a = ((20 / dist) * (20 / dist)) * .12f;
                        cpcolor2.a = ((20 / dist) * (20 / dist)) * .12f;
                    }
                    else
                    {
                        cpcolor.a = .4f;
                        cpcolor2.a = .4f;
                    }

                }
                CP_tracking.color = cpcolor;
                CP_trackingII.color = cpcolor2;
            }



        }
        else if (CatchPointTime >= 40&& Cap==false)//============================================= Cap Success
        {
            Debug.Log("Finish - Success Capture Control Point | 完 成 - 控 制 點 佔 領 成 功");

            if (OBJ隱藏 != null) { OBJ隱藏.SetActive(false); }

            if (OBJ顯示 != null) { OBJ顯示.SetActive(true); }

            if(OBJ動畫Trigger!= null) { OBJ動畫Trigger.SetTrigger("Trigger"); }

            Vector3 v3 = new Vector3(0, 2.32f, 0);
            Destroy(Instantiate(CPcap, transform.position + v3, Quaternion.identity), 1);
            Cap = true;
            Destroy(CPUI.gameObject);
            point_image.sprite = point_image_on;
        }
        //======================================================================================= C P Point
        if (Time.time > tick && Cap!= true)
        {
            tick = Time.time + .2f;

            if (onPoint == true)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                     view.RPC("CatchPointTimeUpdate", RpcTarget.All);
                }

            }
            else if (leave <= 0 && CatchPointTime > 0 && onPoint == false)
            {
                //CatchPointTime -= 1;
                if (PhotonNetwork.IsMasterClient)
                {
                    view.RPC("CatchPointTimeUpdate_Del", RpcTarget.All);
                }
            }

        }

        if (leave < 4.5f && Cap != true)
        {
            onPoint = false;
            CPUIcap.SetActive(false);
        }
        if(leave > 0 && Cap != true)
        {
            leave -= Time.deltaTime;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (view.IsMine)
        {
            if (collision.tag == "PlayerTrigger")
            {
                view.RPC("CatchPointonPoint", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void CatchPointonPoint()
    {
        onPoint = true;
        leave = 5;
    }


        [PunRPC]
    void CatchPointTimeUpdate()
    {
        CatchPointTime += 1;
        CPUIcap.SetActive(true);
    }

    [PunRPC]
    void CatchPointTimeUpdate_Del()
    {
        CatchPointTime -= 1;

    }
}
