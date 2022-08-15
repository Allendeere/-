using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "System_Mission", menuName = "New System/System_MenuSetting")]
public class Setting : ScriptableObject
{
    [Header("¾_°Ê")]
    public bool IsVibration;
    [Header("³qª¾")]
    public bool IsNotifications;
}
