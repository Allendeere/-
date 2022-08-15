using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector2 lastCheckPointPos;
    public M_Vote mv;
    ReLoad reLoad;
    private void Awake()
    {
        reLoad = GameObject.Find("ReLoad").GetComponent<ReLoad>();
        lastCheckPointPos = reLoad.transform_;
        
        if (instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(instance); //<���欰���s���J�ᤣ�R������
        }
        else
        {
            Destroy(gameObject);
        }

        reLoad.transform_ = new Vector2(-5, -2);
    }
    // Start is called before the first frame update

}
