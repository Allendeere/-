using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryP : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "character")
        {
            Destroy(player);
        }
    }

}
