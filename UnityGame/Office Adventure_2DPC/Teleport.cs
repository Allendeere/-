using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Portal;
    public GameObject Player;
    public Animator cutscreen = null;
    public GameObject BossHealthPanel = null;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(tp());

            if(cutscreen != null) { 

            cutscreen.SetBool("cutscreen01", true);
            GameObject.Find("gun").GetComponent<weapon>().enabled = false;
            Invoke(nameof(delay), 3f);

            }
           
            
            
        }
    }

    IEnumerator tp()
    {
        yield return new WaitForSeconds(0);
        Player.transform.position = new Vector2(Portal.transform.position.x, Portal.transform.position.y);

    }

    void delay()
    {
        BossHealthPanel.SetActive(true);
        GameObject.Find("gun").GetComponent<weapon>().enabled = true;
        GameObject.Find("Boss").GetComponent<BossAI>().whichLevel();
    }
}
