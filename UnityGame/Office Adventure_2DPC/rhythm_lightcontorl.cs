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
    public float ���� = 1;
     float lightdelay;

  

    [SerializeField]  trap_move[] �긹;
    [SerializeField] trap_move[] �p���^;

    [SerializeField] Spike_Spawn_Timmer[] ��;
    bool �x; bool ��; bool �j;

    [SerializeField] rhythm ry;

    bool sec;
    bool secII;
    void Start()
    {
        if (�� != null)
        {
            for (int i = 0; i < ��.Length; i++)
            {
                if (��[i].ID == 1) { �x = true; }
                if (��[i].ID == 2) { �� = true; }
                if (��[i].ID == 3) { �j = true; }
            }
        }
        lightdelay = ����;
    }
    private void Update()
    {
        lightdelay -= Time.deltaTime;//.5f
        if (lightdelay < 0)
        {
            sec = !sec;
            lightdelay = ����;
            ry.amm�֥[��();
            if (�x)
            {
                S�x��();
            }

                if (sec)
            {
                �I�O();
                
                if (��)
                {
                    Tom����();
                }

                    secII = !secII;
                if (secII)
                {
                    if (�j)
                    {
                        Floor�j��();
                    }
                }
            }

        }


 

        if (�긹 != null)
        {
            foreach (trap_move tm in �긹)
            {
                tm.MoveTrap();
            }
        }
        if (�p���^ != null)
        {
            foreach (trap_move vio in �p���^)
            {
                vio.RotationTrap();
            }
        }
    }
    public void �I�O()
    {
        if (pathcolor != null)
        {
            Invoke(nameof(���O), .2f);
            c.a = 1;
            pathcolor.color = c;
        }

        if (�긹 != null)
        {
            foreach (trap_move tm in �긹)
            {
                if (tm.transform.position.x == tm.EndPos.x && tm.transform.position.y == tm.EndPos.y)
                {
                    tm.move = true;
                    tm.speed_up = 0;
                    tm.�I������();
                }
                else if (tm.transform.position.x == tm.StartPos.x && tm.transform.position.y == tm.StartPos.y)
                {
                    tm.move = false;
                    tm.speed_up = 0;
                    tm.�I������();
                }
            }
        }
        if (�p���^ != null)
        {
            foreach (trap_move vio in �p���^)
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

    public void ���O()
    {
        c.a = .4f;
        pathcolor.color = c;
    }

    void Floor�j��()
    {
        foreach (Spike_Spawn_Timmer s in ��)
            {
            if (s.ID == 3) { s.TriggerSpike(); }
               
            }
    }

    void Tom����()
    {
            foreach (Spike_Spawn_Timmer s in ��)
            {
            if (s.ID == 2) { s.TriggerSpike(); }
            
            }
    }

    void S�x��()
    {
        foreach (Spike_Spawn_Timmer s in ��)
            {
            if (s.ID == 1) { s.TriggerSpike(); }
        }
    }
}
