using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "System_Mission", menuName = "New System/System_MenuSetting")]
public class Setting : ScriptableObject
{
    [Header("�_��")]
    public bool IsVibration;
    [Header("�q��")]
    public bool IsNotifications;
}
