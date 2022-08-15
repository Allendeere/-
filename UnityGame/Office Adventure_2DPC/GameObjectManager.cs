using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    //Holds instance of GameObjectManager

    [Header("³s ±µ : SpawnPlayers")]
    public static GameObjectManager instance = null;
    public List<GameObject> allObjects = new List<GameObject>();

    void Awake()
    {
        //If this script does not exit already, use this current instance
        if (instance == null)
            instance = this;

        //If this script already exit, DESTROY this current instance
        else if (instance != this)
            Destroy(gameObject);
    }
}
