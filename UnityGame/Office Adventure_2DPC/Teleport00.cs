using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport00 : MonoBehaviour
{
  
    public Animator CutScreen;
    public GameObject BossHealthPanel;

    public Animator BossAnimation;
    public GameObject BossHitBox;
    public Animator hitboxAnimation;
    public float delaytime;

    public GameObject barrier;

    private Collider2D mycollider;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "character")
        {
            CutScreen.SetBool("cutscreen01", true);
            Invoke(nameof(delay), delaytime);

            mycollider =  GetComponent<Collider2D>();
            mycollider.enabled = false;

            if (BossAnimation != null) {
                BossAnimation.SetInteger("anim", 1);
                hitboxAnimation.SetInteger("anim", 1);
                barrier.SetActive(true);
                BossHitBox.SetActive(true);
             
            }
            
        }
    }


    void delay()
    {
        BossHealthPanel.SetActive(true);
        GameObject.Find("Boss").GetComponent<BossAI>().whichLevel();
    }
}
