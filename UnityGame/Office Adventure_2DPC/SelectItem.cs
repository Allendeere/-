using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    public inventorymanager im;
    public GameObject nextplant;
    [SerializeField] GameObject lettleplant;

    public SoulSelect souls;



    [SerializeField] item item;
    
    // Start is called before the first frame update

    public void Del()
    {
        if (im.item == null)
            return;

        item = im.item;
        if(item.itemHeld > 0)
        {
            item.itemHeld -= 1;
        }

        for (int i = 0; i < im.myBag.itemList.Count; i++)
        {
            if (im.myBag.itemList[i] != null && im.myBag.itemList[i].itemHeld <= 0)
            {
                im.myBag.itemList[i] = null;
            }
        }

        inventorymanager.RefreshItme();
    }

    public void Fild()
    {
     if(im.itemname== "Letter") //贱纘獺ン
        {
            for (int i = 0; i < im.myBag.itemList.Count-1; i++)
            {
                Debug.Log(im.myBag.itemList[i]);
                if (im.myBag.itemList[i] == null) {  }
                else if (im.myBag.itemList[i].itemName == "Key")
                {
                    lettleplant.SetActive(true);
                    im.myBag.itemList[i].itemHeld -= 1;
                    Del();
                    break;
                }
            }
        } 
     else if (im.itemname == "Key") { Debug.Log("芲巴"); }
    

        else if (im.itemname == "Soul_kubu")
        {
            souls.pnum = 2;
            nextplant.SetActive(true);
            //Debug.Log("畐ガ艶活");
        }

        else if (im.itemname == "Soul_universe ")
        {
            souls.pnum = 3;
            nextplant.SetActive(true);
            Debug.Log("﹝艶活");
        }

        else if (im.itemname == "Soul_bubbleping")
        {
            souls.pnum = 4;
            nextplant.SetActive(true);
            Debug.Log("獁獁肞艶活");
        }

        else if (im.itemname == "Soul_ladybug")
        {
            souls.pnum = 5;
            nextplant.SetActive(true);
            Debug.Log("縘挛艶活");
        }
    }
}
