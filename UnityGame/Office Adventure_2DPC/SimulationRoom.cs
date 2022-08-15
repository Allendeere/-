using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SimulationRoom : MonoBehaviour
{
    [SerializeField] int state;
    [SerializeField] playerinfo pinfo;

    [SerializeField] Animator an;

    [SerializeField] Slider slider;

    [SerializeField] GameObject lvup;
    int value;
    [SerializeField] GameObject st4;
    float wait;

    [SerializeField] GameObject rob;

    [SerializeField] M_Vote mv;
    PhotonView view;
    int vv=1;
    float delay=.2f;
    void Start()
    {
        state = 1; 
        an.SetInteger("State", 1);
        view = mv.view;
    }

    void Update()
    {
        if (gameObject.tag == "Portal" && Time.time > wait)
        {
            if (state == 1 )
            {Debug.Log("walk");
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
                { view.RPC("updatevalue", RpcTarget.All); }
            }
            if (state == 2)
            {Debug.Log("dash");
                if (Input.GetKeyDown(KeyCode.LeftShift))
                { view.RPC("updatevalue", RpcTarget.All); }
            }
            if (state == 3)
            {Debug.Log("space");
                if (Input.GetKeyDown(KeyCode.Space))
                { view.RPC("updatevalue", RpcTarget.All); }
            }
            if (state == 4)
            { Debug.Log("mouse");
                /*if (Input.GetButton("Fire1"))
                { value += 3; wait = Time.time + .5f; }*/
                if (rob == null)
                {
                    view.RPC("updatevalue", RpcTarget.All);
                }

            }
            if (state == 5)
            {Debug.Log("R - Click");
                if (Input.GetMouseButtonDown(1))
                { view.RPC("updatevalue", RpcTarget.All); }
            }
            if (state >= 6) {
                Debug.Log("Fin"); Invoke(nameof(a), 4f);
                state++;
            }
            slider.value = (value);

            if (slider.value == slider.maxValue)
            {
                view.RPC("stateupdate", RpcTarget.All);

            }
        }
    }

    [PunRPC]
    void updatevalue()
    {
        value+= vv; 
        wait = Time.time + delay;
    }
    [PunRPC]
    void stateupdate()
    {
        value = 0;
        slider.value = value;
        state++;
        an.SetInteger("State", state);
        an.SetTrigger("Done");

        if (state == 2)
        {
            vv = 7;
        }
        if (state == 3)
        {
            vv = 10;
        }
        if (state == 4)
        {
            vv = 20;
            delay = .5f;
            st4.SetActive(true);
            rob.SetActive(true);
        }
        if (state == 5)
        {
            pinfo.SuperAttack = 30; 
            st4.SetActive(false);
        }
    }

    void a()
    {
        Debug.Log("Fin");
        lvup.SetActive(true);
        slider.enabled = false;
    }

}

