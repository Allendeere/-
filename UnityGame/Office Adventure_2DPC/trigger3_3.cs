using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger3_3 : MonoBehaviour
{

    [SerializeField] Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerTrigger")
        {
            animator.SetTrigger("Trigger");
        }
    }
}
