using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestory : MonoBehaviour
{
    [Header("����R��")]
    [SerializeField] int DestoryDelay = 2;
    void Start()
    {
        Destroy(this.gameObject, DestoryDelay);
    }

}
