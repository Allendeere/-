using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class rhythm_lightcontorl : MonoBehaviour
{
    [SerializeField]
    Tilemap pathcolor;
    Color c = Color.white;

    [Header("TrapMove")]
    public float 延遲 = 1;
     float lightdelay;

  

    [SerializeField]  trap_move[] 圓號;
    [SerializeField] trap_move[] 小提琴;

    [SerializeField] Spike_Spawn_Timmer[] 鼓;
    bool 軍; bool 中; bool 大;

    [SerializeField] rhythm ry;

    bool sec;
    bool secII;
    void Start()
    {
        if (鼓 != null)
        {
            for (int i = 0; i < 鼓.Length; i++)
            {
                if (鼓[i].ID == 1) { 軍 = true; }
                if (鼓[i].ID == 2) { 中 = true; }
                if (鼓[i].ID == 3) { 大 = true; }
            }
        }
        lightdelay = 延遲;
    }
    private void Update()
    {
        lightdelay -= Time.deltaTime;//.5f
        if (lightdelay < 0)
        {
            sec = !sec;
            lightdelay = 延遲;
            ry.amm累加器();
            if (軍)
            {
                S軍鼓();
            }

                if (sec)
            {
                點燈();
                
                if (中)
                {
                    Tom中鼓();
                }

                    secII = !secII;
                if (secII)
                {
                    if (大)
                    {
                        Floor大鼓();
                    }
                }
            }

        }


 

        if (圓號 != null)
        {
            foreach (trap_move tm in 圓號)
            {
                tm.MoveTrap();
            }
        }
        if (小提琴 != null)
        {
            foreach (trap_move vio in 小提琴)
            {
                vio.RotationTrap();
            }
        }
    }
    public void 點燈()
    {
        if (pathcolor != null)
        {
            Invoke(nameof(關燈), .2f);
            c.a = 1;
            pathcolor.color = c;
        }

        if (圓號 != null)
        {
            foreach (trap_move tm in 圓號)
            {
                if (tm.transform.position.x == tm.EndPos.x && tm.transform.position.y == tm.EndPos.y)
                {
                    tm.move = true;
                    tm.speed_up = 0;
                    tm.碰撞煙霧();
                }
                else if (tm.transform.position.x == tm.StartPos.x && tm.transform.position.y == tm.StartPos.y)
                {
                    tm.move = false;
                    tm.speed_up = 0;
                    tm.碰撞煙霧();
                }
            }
        }
        if (小提琴 != null)
        {
            foreach (trap_move vio in 小提琴)
            {
                if (vio.transform.eulerAngles == vio.StartRTS )
                {
                    vio.move = false;
                    vio.speed_up = 0;
                }
                else if (vio.transform.eulerAngles == vio.EndRTS)
                {
                    vio.move = true;
                    vio.speed_up = 0;
                }
            }
        }
    }

    public void 關燈()
    {
        c.a = .4f;
        pathcolor.color = c;
    }

    void Floor大鼓()
    {
        foreach (Spike_Spawn_Timmer s in 鼓)
            {
            if (s.ID == 3) { s.TriggerSpike(); }
               
            }
    }

    void Tom中鼓()
    {
            foreach (Spike_Spawn_Timmer s in 鼓)
            {
            if (s.ID == 2) { s.TriggerSpike(); }
            
            }
    }

    void S軍鼓()
    {
        foreach (Spike_Spawn_Timmer s in 鼓)
            {
            if (s.ID == 1) { s.TriggerSpike(); }
        }
    }
}
