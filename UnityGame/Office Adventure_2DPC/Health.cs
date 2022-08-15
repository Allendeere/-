using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Health : MonoBehaviour
{
    [SerializeField] PhotonView view;

    public int health;
    public int numofHearts;

    public GetComponent getComponent;
    [SerializeField] Animator Takedamage;
    [SerializeField] Animator CharaecterTakedamage;
    Animator Rhythm_Takedamage;
    [SerializeField] PlayerPos playpos;
    [SerializeField] weapon Weapon;
    public Image[] hearts;

    public Image heartbar;
    public Sprite[] heartbars;

    public Sprite fullHeart;
    public Sprite emptyHeart;
    public playermovement plm;

    public GameObject playerPerfab;
    public GameObject thisplayer;
    public GameObject healthui;

    bool TakeDamageInvincible;
    void Start()
    {
        if (view.IsMine)
        {
            //Health 設定
            getComponent = GameObject.Find("LevelLoad").GetComponent<GetComponent>();
            getComponent.healthui = healthui;
            heartbar = getComponent.heartbar;
            hearts[0] = getComponent.hearts[0];
            hearts[1] = getComponent.hearts[1];
            hearts[2] = getComponent.hearts[2];
            Takedamage = getComponent.Takedamage;
            Rhythm_Takedamage = getComponent.Rhythm_Takedamage;

            getComponent.Playerobject = thisplayer;
            getComponent.hl = this;
            //playermovemetn 設定
            if (Weapon != null)
            {
                Weapon.Rhythm_HIT = getComponent.Rhythm_HIT;
                Weapon.nUItouch = getComponent.note_UI;
            }
            plm.pinfo = getComponent.pinfo;
            plm.Start_();

            //PlayerPos 設定
            playpos.gm = getComponent.gm;
            playpos.Start_();

            //GetComponent 傳送Weapon
            if (Weapon != null)
            {
                getComponent.Weapon = Weapon;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (health > numofHearts)
            {
                health = numofHearts;
            }
            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < health)
                {
                    hearts[i].sprite = fullHeart;
                }
                else { hearts[i].sprite = emptyHeart; }

                if (i < numofHearts)
                {
                    hearts[i].enabled = true;
                }
                else { hearts[i].enabled = false; }
            }
            if(health >= 3 && health <= 1) { heartbar.sprite = heartbars[health]; }
        }
    }
     public void TakeDamage(int amount)
    {
        if (view.IsMine)
        {
            if (plm.Invincible == false|| !TakeDamageInvincible)
            {
                health -= amount;
                TakeDamageInvincible = true;
                Invoke(nameof(T_TakeDamageInvincible),1.5f);
                Takedamage.SetTrigger("TakeDamage");
                CharaecterTakedamage.SetTrigger("TakeDamage");
                playpos.TriggerCheckPoint();

                if (health <= 0)
                {
                    Rhythm_Takedamage.SetTrigger("Dead");
                    PhotonNetwork.Instantiate(playerPerfab.name, this.transform.position, Quaternion.identity);
                    PhotonNetwork.Destroy(thisplayer);
                    GameObjectManager.instance.allObjects.Remove(gameObject);
                }
            }
            else if (plm.Invincible == true|| TakeDamageInvincible)
            { Debug.Log("無敵"); }
        }
    }

    void T_TakeDamageInvincible()
    {
        TakeDamageInvincible = false;
    }

}
