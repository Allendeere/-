using UnityEngine;
using UnityEngine.Events;
public class DashMovement : MonoBehaviour
{

    public float DashForce;
    public float StartDashTimer;
    float DashDirection;
    float CurrentDashTimer;
    //**********************************
    public Animator head;
    public Animator feet;
    public Animator body;
    //**********************************
    bool isDashing;
    Rigidbody2D rb;
    private Vector2 movX;

    public float cooldown = 0.5f;
    private float nextdash = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 gunPos = transform.position;
     
        movX = gunPos;
            
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > nextdash)
        {
            nextdash = Time.time + cooldown;
            isDashing = true;
            CurrentDashTimer = StartDashTimer;
            rb.velocity = Vector2.zero;
            //**********************************
            if (isDashing == true)
            {
                head.SetBool("Dashing", true);
                feet.SetBool("Dashing", true);
                body.SetBool("Dashing", true);
                body.SetFloat("Speed", 0);
                head.SetFloat("Speed", 0);
                feet.SetFloat("Speed", 0);
            }
            //**********************************

        }

        if (isDashing)
        {
            rb.velocity = transform.right *  DashForce;
            CurrentDashTimer -= Time.deltaTime;
            if(CurrentDashTimer <= 0)
            {
                isDashing = false;
                //**********************************
                if (isDashing == false)
                {
                    head.SetBool("Dashing", false);
                    feet.SetBool("Dashing", false);
                    body.SetBool("Dashing", false);
                }
                //**********************************
            }
        }
    }
}
