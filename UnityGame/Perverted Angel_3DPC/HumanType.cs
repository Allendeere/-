using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "----------HumanType", menuName = "0-0/=----------HumanType")]
public class HumanType : ScriptableObject
{
    [Header("角色名稱")]
    public string Name;

    [Header("總血量")]
    public int HP;

    [Header("部位血量")]
    public int 頭;
    public int 胸;
    public int 臀;
    public int 左大;
    public int 左小;
    public int 右大;
    public int 右小;
}
