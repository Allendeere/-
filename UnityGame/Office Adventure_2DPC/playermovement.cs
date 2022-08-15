using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class playermovement : MonoBehaviourPunCallbacks//, M_CharData
{
    [SerializeField] private LayerMask m_WhatIsGround;
    public CharacterController2D controller;
    public PhotonView view;
    //-------------------------------------------------===< 動 畫 >===-----------------
    public playerinfo pinfo;
    public Animator Feet;
    public Transform GroundCheck;
    public Transform faseing;
    [SerializeField] ParticleSystem collectparticle = null;
    public Rigidbody2D rb;
    public Health hl;
    public weapon Weapon;
    [SerializeField] HeavyAttack Heavyattack;

    //-------------------------------------------------===< 移 動 >===-----------------
    public float horizontalMove = 0f;
    public int runSpeed = 25;

    bool jumplock = true;
    public bool crouch = false;


    const float k_GroundedRadius = .2f;
    private bool m_Grounded;

    //-------------------------------------------------===< 變 身 >===-----------------
    public bool switching = true;
    public float nextswitch = 0.0f;

    public int Private_can_num;
    public GameObject switchEffect;
    //-------------------------------------------------===< 衝 刺 >===-----------------

    public int DashForce;
    int DashJump;
    public float StartDashTimer;
    public float cooldown = 0.5f;

    private float nextdash = 0.0f;
    public float damageRate = 0.5f;
    private float damagecooldown = 0.0f;

    float CurrentDashTimer;
    bool isDashing;

    public bool TeleportDash;
    public bool Invincible;

    public bool jump = false;
    // ReLoad reLoad;
    [SerializeField] GameObject gobjthis;

    public RuntimeAnimatorController[] CAn;
    [SerializeField] Item_Info[] Items;

    //private void Start() { }

    public void Start_()
    {
        Filter();
        switching = !switching;
        nextswitch = Time.time + 1;
    }
    void Update()
    {

        //---------------------------------------------------------------【衝 刺】------------------------------------------------------------------
        if (view.IsMine)
        {
            //     reLoad.transform_ = this.transform.position;
            if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > nextdash)
            {
                nextdash = Time.time + cooldown;
                isDashing = true;
                CurrentDashTimer = StartDashTimer;
                horizontalMove = 0;

                if (isDashing == true)
                {
                    Feet.SetBool("Dashing", true);
                    Feet.SetFloat("Speed", 0);
                }
            }
            if (isDashing)
            {
                if (TeleportDash) { Invincible = true; }
                if (faseing.eulerAngles.y > 90)
                {
                    rb.velocity = Vector2.right * DashForce + Vector2.up * DashJump;
                }
                else { rb.velocity = Vector2.left * DashForce + Vector2.up * DashJump; }
                CurrentDashTimer -= Time.deltaTime;
                if (CurrentDashTimer <= 0)
                {
                    isDashing = false;
                    if (isDashing == false)
                    {
                        if (TeleportDash) { Invincible = false; }
                        Feet.SetBool("Dashing", false);
                    }
                }
            }
            //-----------------------------------------------------------【變 身】------------------------------------------------------------------
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextswitch)
            {
                Filter();
                SwitchEffectCall();
                switching = !switching;
                nextswitch = Time.time + 1;
            }

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            Feet.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Feet.GetBool("IsCrouching") && jumplock == false)
            {
                jumplock = true;
            }
            else if (Feet.GetBool("IsCrouching") == false && jumplock == true)
            {
                jumplock = false;
            }

            if (jumplock == false)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    jump = true;

                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                jump = false;
            }

            if (jump == false)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    crouch = true;
                }
                else if (Input.GetKeyUp(KeyCode.S))
                {
                    crouch = false;
                }
            }
            if (jump == true)
            {
                Feet.SetBool("IsJumping", true);
            }
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)//如果落地
                {
                    Feet.SetBool("IsJumping", false);
                    Feet.SetBool("reJump", true);
                }
                else if (wasGrounded)
                {
                    if (Feet.GetBool("reJump"))
                    {
                        Feet.SetBool("reJump", false);
                    }
                }
            }
            Feet.SetBool("IsJumping", false);
        }
    }
    public void OnLanding()
    {
        Feet.SetBool("IsJumping", false);
        collect();
    }
    public void OnCrouching(bool isCrouching)
    {
        Feet.SetBool("IsCrouching", isCrouching);
    }
    public void collect()
    {
        collectparticle.Play();
    }
    private void OnTriggerStay2D(Collider2D collision)//-----------------【碰 撞】------------------------------------------------------------------
    {
        if (collision.name == "SpeedBoot-up")
        {
            rb.velocity = transform.right * runSpeed + transform.up * 10;
            FindObjectOfType<AudioManger>().Play("Boot01");

        }
        if (collision.name == "SpeedBoot-down")
        {
            rb.velocity = transform.right * runSpeed + Vector3.down * 60;
            FindObjectOfType<AudioManger>().Play("Boot");
        }
        if (collision.name == "SpeedBoot-from")
        {
            rb.velocity = transform.right * 45 + transform.up * 2;
            FindObjectOfType<AudioManger>().Play("Boot");
        }
        if (collision.tag == "traps" && Time.time > damagecooldown)
        {
            hl.TakeDamage(1); Debug.Log("OnTriggerEnter2D" + collision);
            damagecooldown = Time.time + damageRate;
        }

    }

    public void SwitchEffectCall()
    {
        Destroy(PhotonNetwork.Instantiate(switchEffect.name, transform.position, transform.rotation), 5);
    }

    public void Filter()//------------------------------------------------【過 濾】--------------------------------
    {
        int p1 = PlayerPrefs.GetInt("P1");
        int p2 = PlayerPrefs.GetInt("P2");

        Debug.Log("玩家一:" + PlayerPrefs.GetInt("P1") + "玩家二:" + PlayerPrefs.GetInt("P2"));

        //Jelly : 0 / Kubu :2 / StarDust : 3 / BubblePing :4

        if (switching == true)//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        {
            Items[p1].Use();// Debug.Log(p1); 
            /*
            if (p1 == 0)
            {
                Items[0].Use();
            }
            else if (p1 == 1)
            {
                Items[1].Use(); Debug.Log("1");
            }

            else if (p1 == 2)
            {
                Items[2].Use(); Debug.Log("2");
            }
            else if (p1 == 3)
            {
                Items[3].Use();
            }
            else if (p1 == 4)
            {
                Items[4].Use();
            }
            else if (p1 == 5)
            {
                Debug.Log("5");
            }*/
        }
        else if (switching == false)//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        {
            Items[p2].Use(); //Debug.Log(p1);
            /*
            if (p2 == 0)
            {
                Items[0].Use();
            }
            else if (p2 == 1) 
            {
                Items[1].Use(); Debug.Log("1");
            }
            else if (p2 == 2) 
            {
                Items[2].Use(); Debug.Log("2");
            }
            else if (p2 == 3)
            {
                Items[3].Use();
            }
            else if (p2 == 4) 
            {
                Items[4].Use();
            }
            else if (p2 == 5) 
            {
                Debug.Log("5"); 
            }
            */
        }

        //-------------------------------------------------------------------------------------------
        if (Weapon != null) { Pm_Weapons(); }
        runSpeed = pinfo.runspeedp;
        DashForce = pinfo.dashspeedp;
        DashJump = pinfo.dashjumpp;
        StartDashTimer = pinfo.dashtimep;
        cooldown = pinfo.dashcoldp;
        TeleportDash = pinfo.dodashteleportp;
        m_Filter(pinfo.Heldp);
    }
    void Pm_Weapons()
    {
        Weapon.bulletPrefab = pinfo.bullect;
        Heavyattack.hbullect = pinfo.super_bullect;
        Weapon.fireRate = pinfo.fireRate;
    }
    //-----------------------------------------------------------------------------------------------

    public void m_Filter(int can_num)
    {
        Private_can_num = can_num;
        Feet.GetComponent<Animator>().runtimeAnimatorController = CAn[Private_can_num];
        if (view.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("Private_can_num", Private_can_num);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!view.IsMine && targetPlayer == view.Owner)
        {
            m_Filter((int)changedProps["Private_can_num"]);
        }
    }

    void OnDestroy()
    {
        SpawnPlayers.WhenDestory();
    }
}

