using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class M_Vote : MonoBehaviour//連接 click
{
    public PhotonView view;

    public static M_Vote m_vote;

    public GameObject Mvote;
    public GameObject Pvote;
    public GameObject SliderOBJ;
    public Slider TimeSlider;
    
    bool V;
    public int click;
    public float votetime = 5;
    public int level;
    ReLoad reLoad;
    [SerializeField] bool inlevel;
    [SerializeField] GameObject ui_plane;
    [SerializeField] GameObject ui_planeII;
    [SerializeField] GetComponent getcomponent;
    [SerializeField] GameObject myPlayer;
    [SerializeField] LVcomplete lvcp;
    [SerializeField] rhythm rhythm;
    [SerializeField] Text t;
    [SerializeField] Animator AnimatorForMenuVotingIsNotFinsh;
    void Start()
    {
        m_vote = this;
        Mvote.SetActive(false);
        Pvote.SetActive(false);
        view = GetComponent<PhotonView>();
        V = false;
        reLoad = GameObject.Find("ReLoad").GetComponent<ReLoad>();
        if (!reLoad.V_)
        {
            ui_plane.SetActive(true);
            reLoad.V_ = true;
            reLoad.F();
            view.RPC("RPC_Reload", RpcTarget.All);
        }
        else if (reLoad.V_)
        {
            rhythm.r_Start();
        }


        if (!inlevel)
        {
            PhotonNetwork.CurrentRoom.IsOpen = true;
            PhotonNetwork.CurrentRoom.IsVisible = true;
            Debug.Log("當前房間狀態 = 公開");
        }
    }

    private void Update()
    {
        if (lvcp != null) { lvcp.TestForPlayerOnIt(); }


        if (V)
        {
            if (votetime > 0)
            {
                votetime -= Time.deltaTime;
                TimeSlider.value = (votetime);
            }
            else
            {
                if (click > 0)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        view.RPC("RPC_Play", RpcTarget.All);
                    }
                    Debug.Log("通過");
                }
                else
                {
                    Debug.Log("沒過");
                    SliderOBJ.SetActive(false);
                    Mvote.SetActive(false);
                    Pvote.SetActive(false);
                }
                V = false;
                votetime = 5;
                click = 0;
            }
        }
    }
    public void Leave()
    {
        //ReLoad reLoad = GameObject.Find("ReLoad").GetComponent<ReLoad>();
        reLoad.leaveroom();
    }
    public void Skip_()
    {
        V = false;
        votetime = 5;
        click = 0;
        view.RPC("RPC_Play", RpcTarget.All);
    }


    void Play_()
    {

            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel(level);

            PhotonNetwork.CurrentRoom.IsOpen = false;
             PhotonNetwork.CurrentRoom.IsVisible = false;
    }
    public void Vote_(int level_) // <-----------------------
    {
        
        if (!V)
        {
            level = level_;
            view.RPC("RPC_Vote", RpcTarget.All,level_);
        }
        else
        {
            AnimatorForMenuVotingIsNotFinsh.SetTrigger("Trigger");
        }
    }
    public void Vote_Text_(string s)
    {
        if (!V)
        {
            view.RPC("RPC_Vote_Text", RpcTarget.All, s);
        }
    }
    void Vote__()
    {
        if (V == false)
        {
            TimeSlider.value = 5;
            V = true;
            SliderOBJ.SetActive(true);
            if (PhotonNetwork.IsMasterClient)
            {
                Mvote.SetActive(true);
            }
            if (!PhotonNetwork.IsMasterClient)
            {
                Pvote.SetActive(true);
            }
        }
        else 
        {
            Debug.Log("投票還沒結束...");
        }
    }

    public void Click_()// <----------------------- V
    {
        click++;
        view.RPC("RPC_Click", RpcTarget.All, click);
        //SliderOBJ.SetActive(false);
        Mvote.SetActive(false);
        Pvote.SetActive(false);

    }

    public void Click_X()// <---------------------- X
    {
        click--;
        view.RPC("RPC_Click", RpcTarget.All, click);
        //SliderOBJ.SetActive(false);
        Mvote.SetActive(false);
        Pvote.SetActive(false);
    }


    public static void click_tp(int type) //連接 click
    {
        m_vote.click_run(type);

    }
    void click_run(int type)
    {
        myPlayer = getcomponent.Playerobject;

        if (type == 1)
        {
            myPlayer.transform.position = new Vector2(120, 13);
        }
        else if (type == 2)
        {
            myPlayer.transform.position = new Vector2(-5, -2);
        }
        else if (type == 3)
        {
            myPlayer.transform.position = new Vector2(5, 28);
        }
    }
    [PunRPC]
    void RPC_Vote(int level_)
    {
        level = level_;
        Debug.Log("Level:" + level);
        Vote__();
    }
    [PunRPC]
    void RPC_Vote_Text(string s)
    {
            t.text = s;
    }
    [PunRPC]
    void RPC_Click(int click_)
    {
        click = click_;
        Debug.Log("Click" + click);
    }
    [PunRPC]
    void RPC_Play()
    {
        Play_();
    }
    [PunRPC]
    void RPC_Reload()
    {
            ui_planeII.SetActive(true);
        myPlayer = getcomponent.Playerobject;
        if (myPlayer != null)
        {
            reLoad.transform_ = myPlayer.transform.position; Debug.Log("aaa11100011");
        }
    }
}
