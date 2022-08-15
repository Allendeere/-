using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class soulmovement : MonoBehaviourPunCallbacks
{
    [SerializeField] bool m_FacingRight = true;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;
    public GameObject cam;
    public PhotonView view;
    //-------------------------------------------------===< °Ê µe >===-----------------
    public Animator Feet;
    public Transform faseing;
    public Rigidbody2D rb;
    //-------------------------------------------------===< ²¾ °Ê >===-----------------
    public float horizontalMove = 0f;
    public int runSpeed = 25;
    public float jumpSpeed = 6;
    //-------------------------------------------------===< ½Ä ¨ë >===-----------------
    public LineRenderer line;
    public DistanceJoint2D joint;
    float move;
    [SerializeField] GameObject fllow;
    bool fllowing;
    float FllowingDelay;
    void Update()
    {
        //---------------------------------------------------------------¡i½Ä ¨ë¡j------------------------------------------------------------------
        if (view.IsMine)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                runSpeed = 50;
                jumpSpeed = 10;
                Feet.SetBool("Dashing", true);
            }
            else
            {
                runSpeed = 25;
                jumpSpeed = 6;
                Feet.SetBool("Dashing", false);
            }
            //------------------------------------------------------------------------

            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            Feet.SetFloat("Speed", Mathf.Abs(horizontalMove));
            move = horizontalMove * Time.fixedDeltaTime;

            if (Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = Vector2.up * jumpSpeed;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                rb.velocity = Vector2.down * jumpSpeed;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                rb.velocity = Vector2.up * 0;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                rb.velocity = Vector2.down * 0;
            }

            //------------------------------------------------------------------------
            if (FllowingDelay > 0)
            {
                FllowingDelay -= Time.deltaTime;
            }
            /*
            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                if (fllowing)
                {
                    fllow = null;
                    joint.enabled = false;
                    line.enabled = false;
                    fllowing = false; 
                }
            }*/

            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (fllowing&& FllowingDelay<=0)
                {
                    fllow = null;
                    joint.enabled = false;
                    line.enabled = false;
                    fllowing = false;
                }

                if (!fllowing && FllowingDelay <= 0) { 

                Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                    
                if (hit.collider != null && (hit.collider.gameObject.tag == "PlayerSoul" || hit.collider.gameObject.tag == "Player")&& hit.collider.gameObject!= this.gameObject)
                {
                        if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
                        {
                            fllow = hit.collider.gameObject;
                            joint.connectedBody = fllow.GetComponent<Rigidbody2D>();
                            joint.enabled = true;
                            line.enabled = true;
                            fllowing = true;

                            FllowingDelay = .8f;
                        }
                }
                else if (hit.collider != null && hit.collider.gameObject.tag == "PlayerTrigger" && hit.collider.gameObject != this.gameObject)
                    {
                            fllow = hit.collider.gameObject;
                            joint.connectedBody = fllow.GetComponentInParent<Rigidbody2D>();
                            joint.enabled = true;
                            line.enabled = true;
                            fllowing = true;

                            FllowingDelay = .8f;
                    }
                    else
                    {
                        Debug.Log("na");
                    }
            }
            }
            
            if (fllowing)// (fllow != null)
            {
                line.SetPosition(0, fllow.transform.position);
                line.SetPosition(1, transform.position);
            }/*
            else if(fllow ==null&& fllowing)
            {
                line.enabled = false;
                fllowing = false;
            }*/
            

            if (joint.enabled)
            {
                line.SetPosition(1, transform.position);
            }

        }
    }
    void FixedUpdate()
    {     
        Vector3 targetVelocity = new Vector2(move* 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        Flip();
        if (move > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (move < 0 && m_FacingRight)
        {
            Flip();
        }
        
    }
    void Flip()
    {
        m_FacingRight = !m_FacingRight;
        if (move > 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            cam.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (move < 0)
        {
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
            cam.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }
    //-----------------¡i¸ò ÀH¡j------------------------------------------------------------------


    /*private void OnTriggerStay2D(Collider2D collision)//-----------------¡i¸I ¼²¡j------------------------------------------------------------------
    {
        if (collision.name == "SpeedBoot-up")
        {
            rb.velocity = transform.right * runSpeed + transform.up * 10;
            FindObjectOfType<AudioManger>().Play("Boot01");

        }
    }*/
}

