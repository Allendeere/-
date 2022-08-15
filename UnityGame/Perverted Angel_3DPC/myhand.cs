using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class myhand : MonoBehaviour
{

    [SerializeField] Image inventory_ui;
    public Image[] item;
    float closetime=3;
    float Alpha;
    bool open;
    public int num;
    [SerializeField] Transform item_pos;
    [SerializeField] Transform hand_pos;
    public List<GameObject> items = new List<GameObject>();
    [SerializeField] Animator handan;
    public float wait;


    public iteminfo iteminfo;
    void Update()
    {


        if ( Input.mouseScrollDelta.y != 0)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                num = num >1 ? 0 : num+1;
            }
            else if(Input.mouseScrollDelta.y < 0)
            {
                num = num < 1 ? 2: num - 1 ;
            }
            ShowmainItem();
            open = true;

            closetime = 3;
        }
        else if(Input.GetMouseButtonDown(1))
        {
            if (items[num] == null)
                return;
            ThrowoutItem();

        }
        else if (Input.GetMouseButton(0)&&Time.time>wait)
        {
            wait = Time.time + 1;
            if (items[num] == null)
                return;
            Attack();
        }
        else
        {
            handan.SetBool("Hit", false);
        }
        
        if (closetime > 0&&open==true)
        {
            Showup();
            if (Alpha < 10)
            {
                Alpha += .1f;
            }
            closetime -= Time.deltaTime;
        }
        else
        {
            if (open != false) { open = false; }
            Showup();
            if (Alpha >0) { Alpha-=.1f; }

            closetime = 3;
        }
    }


    void Showup()
    {
        
        var Alphaini = inventory_ui.color;
        Alphaini.a = Alpha;
        inventory_ui.color = Alphaini;

        foreach (var n in item)
        {
            var Alphatoui = n.color;
            if (Alpha > 0)
            {
                if (n.sprite == null) 
                {
                    Alphatoui.a = 0f;
                }
                else
                {
                    Alphatoui.a = .4f;
                }

            }
            else
            {
                Alphatoui.a = Alpha;
            }
            n.color = Alphatoui;
        }


        item[num].color = Alphaini;

    }


    public void ShowmainItem()
    {
        foreach (GameObject item in items)
        {
            if (item != null)
            {
                item.SetActive(false);
            }
        }
        if (items[num] != null)
        {
            items[num].SetActive(true);
            iteminfo = items[num].GetComponent<iteminfo>();
        }
    }

    void ThrowoutItem()
    {
        items[num].transform.position = hand_pos.position;
        items[num].transform.parent = item_pos;
        items[num].GetComponent<BoxCollider>().enabled = true;
        items[num].GetComponent<Rigidbody>().isKinematic = false;
        items[num] = null;
        item[num].sprite = null;
    }

    void Attack()
    {
        handan.SetBool("Hit",true);
    }
}
