using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerinfo : MonoBehaviour
{   
    [Header("基本數值")]
    public int Heldp;
    public int runspeedp;
    public int dashspeedp;
    public int dashjumpp;
    public float dashtimep;
    public float dashcoldp;
    public bool dodashteleportp;
    [Header("武器")]
    public GameObject bullect;
    public GameObject super_bullect;
    public float fireRate;


    [Header("必殺技")]
    public int SuperAttack;
    public UI_HeavyAttack SuperAttack_;


    [Header("攻擊加成")]
    public int Damage;
    public int Melee;
    public int Bullect;

    private void Start()
    {
        if (SuperAttack_ != null)
        {
            SuperAttack_.MaxValue(12);
            SuperAttack_.SetValue(0);
        }

        Melee = PlayerPrefs.GetInt("MeleeDamage");
        Bullect = PlayerPrefs.GetInt("BullectDamage");
    }
    private void Update()
    {
        if (SuperAttack_ != null)
        {
            SuperAttack_.SetValue(SuperAttack);
        }
    }

}
