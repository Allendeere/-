using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "System_Mission", menuName = "New System/System_Mission")]
public class Mission : ScriptableObject
{
    [Header("�U�ӥ��� - �ɶ�")]
    [Header("")]
    public int min , hour;
    public bool missionLock;
    [Header("�����`��")]
    public int totalmission;
}
