using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "System_Inventory", menuName = "New System/System_Inventory")]
public class Inventory : ScriptableObject
{
    public List<GameObject> inventory = new List<GameObject>();
}
