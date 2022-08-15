using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    public GameMaster gm;
    public GameObject player;

    public void Start_()
    {
        transform.position = gm.lastCheckPointPos;
    }

    public void TriggerCheckPoint()
    {
        player.transform.position = gm.lastCheckPointPos;
    }
}
