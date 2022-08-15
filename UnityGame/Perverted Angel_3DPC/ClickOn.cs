using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickOn : MonoBehaviour
{
    [SerializeField] GameObject lastHit;
    [Header("準心")]
    [SerializeField] Image my_aim;

    [SerializeField] Sprite default_aim;
    [SerializeField] Sprite item_aim;
    [SerializeField] Sprite human_aim;

    [SerializeField] Text item_name;
    [Header("持有")]
    [SerializeField] Transform my_hand;
    [SerializeField] myhand mh;
    //[SerializeField] GameObject[] itemsobj;
    bool putin;

    [SerializeField] Human human;

    iteminfo iteminfo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(origin: this.transform.position, direction: this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance: 3) && hit.collider.gameObject.layer == LayerMask.NameToLayer("item"))
        {
            if (my_aim.sprite != item_aim) { my_aim.sprite = item_aim; }
            lastHit = hit.transform.gameObject;
            item_name.text = lastHit.name;

            if (Input.GetMouseButtonDown(0))
            {
                iteminfo = lastHit.GetComponent<iteminfo>();
                if (mh.item[mh.num].sprite == null) 
                {
                    //itemsobj[mh.num] = lastHit;
                    
                    mh.item[mh.num].sprite = iteminfo.item.image;
                    mh.items[mh.num] = lastHit;
                    putin = true;

                    mh.ShowmainItem();
                }
                else
                {

                    for (int i = 0; i < mh.items.Count; i++)
                    {
                        if (mh.items[i] == null)
                        {
                            mh.items[i] = lastHit;
                            mh.item[i].sprite = iteminfo.item.image;
                            putin = true;
                            break;
                        }
                    }

                    mh.ShowmainItem();
                }
                if (putin == true)
                {
                    lastHit.transform.position = my_hand.position;
                    lastHit.transform.rotation = my_hand.rotation;
                    lastHit.transform.parent = my_hand;
                    lastHit.GetComponent<BoxCollider>().enabled = false;
                    lastHit.GetComponent<Rigidbody>().isKinematic = true;
                }
                putin = false;

            }

        }

        else if (Physics.Raycast(ray, out hit, maxDistance: 5) && hit.collider.gameObject.layer == LayerMask.NameToLayer("human"))
        {
            if (my_aim.sprite != human_aim) { my_aim.sprite = human_aim; }
            lastHit = hit.transform.parent.gameObject;
            human = lastHit.GetComponent<Human>();
            item_name.text = lastHit.name;

            if (Input.GetMouseButton(0)&& Time.time > mh.wait&& mh.items[mh.num] != null)
            {
                string s = hit.collider.gameObject.name;
                int damage = mh.iteminfo.item.Damage;
                human.activate(s,damage);
            }
        }

        else
        {
            if (my_aim.sprite != default_aim) {  my_aim.sprite = default_aim; }
            lastHit = null;
            item_name.text = "";
        }
    }
}
