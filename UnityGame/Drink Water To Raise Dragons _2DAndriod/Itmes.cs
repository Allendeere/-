using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory_Items", menuName = "New Components/Inventory_Items")]
public class Itmes : ScriptableObject
{
    public string ItemName;
    public int ItemHeld;
    public Sprite ItemImage;
}
