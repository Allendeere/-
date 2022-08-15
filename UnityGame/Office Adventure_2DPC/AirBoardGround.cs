using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AirBoardGround : MonoBehaviour
{
    [SerializeField] EdgeCollider2D boxCollider2D;

    [SerializeField] GetComponent getcom;


    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
            if (collision.name == "ceilingcheck"&& collision.gameObject.transform.parent.gameObject == getcom.Playerobject)
            {
                boxCollider2D.enabled = true;
            }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            if (collision.name == "ceilingcheck" && collision.gameObject.transform.parent.gameObject == getcom.Playerobject)
            {
                boxCollider2D.enabled = false;
            }
        
    }
}
