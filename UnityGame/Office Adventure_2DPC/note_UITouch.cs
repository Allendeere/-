using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class note_UITouch : MonoBehaviour
{
    [SerializeField] GetComponent getComponent;
    int num;
    //[SerializeField] rhythm Rhythm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "note")
        {
            num = 1;
        }
    }

    public void used()
    {
       if(num!=1)
        {
            getComponent.Weapon.Melee();Debug.Log("dddddddddddddddddd");
        }
        num = 0; Debug.Log("dddddddddddddddddd");
    }
}
