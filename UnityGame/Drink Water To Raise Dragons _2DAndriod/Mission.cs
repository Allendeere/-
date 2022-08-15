using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "System_Mission", menuName = "New System/System_Mission")]
public class Mission : ScriptableObject
{
    [Header("下個任務 - 時間")]
    [Header("")]
    public int min , hour;
    public bool missionLock;
    [Header("任務總數")]
    public int totalmission;
}
