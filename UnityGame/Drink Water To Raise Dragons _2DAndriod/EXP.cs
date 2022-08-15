using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "System_EXP", menuName = "New System/System_EXP")]
public class EXP : ScriptableObject
{
    [Header("����")]
    public int Level;
    [Header("�g���")]
    public int Experience;
}
