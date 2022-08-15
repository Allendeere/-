using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_gui : MonoBehaviour
{
    public GameObject myBag;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OpenMyBag();
    }

    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            myBag.SetActive(isOpen);
        }

    }

    }
