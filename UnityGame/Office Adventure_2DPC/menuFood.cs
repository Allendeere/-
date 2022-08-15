using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuFood : MonoBehaviour
{
    public GameObject food1;
    public GameObject food2;
    public GameObject food3;
    public GameObject food4;
    public int getlv;
    // Start is called before the first frame update
    public void Start()
    {
        getlv = PlayerPrefs.GetInt("levelReached");
        if (getlv >= 2) { food1.SetActive(true); }
        if (getlv >= 4) { food2.SetActive(true); }
        if (getlv >= 6) { food3.SetActive(true); }
        if (getlv >= 8) { food4.SetActive(true); }
    }


    // Update is called once per frame

}
