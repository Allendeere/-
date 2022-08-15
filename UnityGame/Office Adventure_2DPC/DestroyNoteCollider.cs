using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNoteCollider : MonoBehaviour
{
    [SerializeField] AudioManger audioManger;
    [SerializeField] int note;
    [SerializeField] GetComponent getComponent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "hit")
        {
            getComponent = GameObject.Find("LevelLoad").GetComponent<GetComponent>();
            audioManger = GameObject.Find("AudioManager").GetComponent<AudioManger>();
            getComponent.Weapon.Shoot();

            audioManger.Play(note.ToString());
            Destroy(this.gameObject);
            Debug.Log("hit");
        }

    }
}
