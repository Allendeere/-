using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] HumanType humanType;
    public int Total_HP;

    public int �Y;
    public int ��;
    public int �v;
    public int ���j;
    public int ���p;
    public int �k�j;
    public int �k�p;

    private void Start()
    {
        Total_HP = humanType.HP;
        �Y = humanType.�Y;
        �� = humanType.��;
        �v = humanType.�v;
        ���j = humanType.���j;
        ���p = humanType.���p;
        �k�j = humanType.�k�j;
        �k�p = humanType.�k�p;
    }

    public void activate(string s , int damage)
    {
        if(s== "�Y")
        {
            float Kill = Random.value;
            if (Kill < .9f)
            {
                �Y -= (int)Mathf.Round(damage * 1.5f);
            }
            else
            {
                �Y = 0;
            }
        }
        else if (s == "��")
        {
            �� -= damage;
        }
        else if (s == "�v")
        {
            �v -= damage;
        }
        else if (s == "���j")
        {
            float Kill = Random.value;
            if (Kill < .98f)
            {
                ���j -= (int)Mathf.Round(damage * 1.3f);
            }
            else
            {
                ���j = 0;
            }
        }
        else if (s == "���p")
        {
            ���p -= damage;
        }
        else if (s == "�k�j")
        {
            float Kill = Random.value;
            if (Kill < .98f)
            {
                �k�j -= (int)Mathf.Round(damage * 1.3f);
            }
            else
            {
                �k�j = 0;
            }
        }
        else if (s == "�k�p")
        {
            �k�p -= damage;
        }

        IfDead();
    }

    void IfDead()
    {
        if (�Y == 0)
        {
            Total_HP = 0;
        }
        else if (�� == 0)
        {

        }
        else if (�v == 0)
        {

        }
        else if (���j == 0)
        {
            
        }
        else if (���p == 0)
        {
            
        }
        else if (�k�j == 0)
        {
            
        }
        else if (�k�p == 0)
        {
            
        }
    }
}
