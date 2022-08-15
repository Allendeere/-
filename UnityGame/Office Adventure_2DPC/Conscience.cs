using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conscience : MonoBehaviour
{
    //¬ÛÃö : Conscience_count ¡A menu_Conscience¡ALevelComplete
    [Header("µ½´c")]
    public int conscience_count;
    [Header("®w¥¬ - Kubo")]
    [SerializeField] int Kubo_kill;
    [SerializeField] int Kubo_tame;
    public Conscience_count _count;
    [Header("ªwªwªQ - BubblePing")]
    [SerializeField] int BubblePing_kill;
    [SerializeField] int BubblePing_tame;

    public void Em_Kill(string a)
    {

        if(a == "Kubo") { Kubo_kill++; }
        else if (a == "BubblePing") { BubblePing_kill++; }


        conscience_count = conscience_count - 2;
    }

    public void Em_Tame(string a)
    {
        if (a == "Kubo") { Kubo_tame++; }
        else if (a == "BubblePing") { BubblePing_tame++; }
        conscience_count = conscience_count + 1;
    }


    public void LevelComplete()
    {
        _count.Kubo_Kill +=  Kubo_kill;
        _count.Kubo_Tame += Kubo_tame;
        _count.Conscience += conscience_count;
    }

}
