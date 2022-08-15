using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] HumanType humanType;
    public int Total_HP;

    public int 頭;
    public int 胸;
    public int 臀;
    public int 左大;
    public int 左小;
    public int 右大;
    public int 右小;

    private void Start()
    {
        Total_HP = humanType.HP;
        頭 = humanType.頭;
        胸 = humanType.胸;
        臀 = humanType.臀;
        左大 = humanType.左大;
        左小 = humanType.左小;
        右大 = humanType.右大;
        右小 = humanType.右小;
    }

    public void activate(string s , int damage)
    {
        if(s== "頭")
        {
            float Kill = Random.value;
            if (Kill < .9f)
            {
                頭 -= (int)Mathf.Round(damage * 1.5f);
            }
            else
            {
                頭 = 0;
            }
        }
        else if (s == "胸")
        {
            胸 -= damage;
        }
        else if (s == "臀")
        {
            臀 -= damage;
        }
        else if (s == "左大")
        {
            float Kill = Random.value;
            if (Kill < .98f)
            {
                左大 -= (int)Mathf.Round(damage * 1.3f);
            }
            else
            {
                左大 = 0;
            }
        }
        else if (s == "左小")
        {
            左小 -= damage;
        }
        else if (s == "右大")
        {
            float Kill = Random.value;
            if (Kill < .98f)
            {
                右大 -= (int)Mathf.Round(damage * 1.3f);
            }
            else
            {
                右大 = 0;
            }
        }
        else if (s == "右小")
        {
            右小 -= damage;
        }

        IfDead();
    }

    void IfDead()
    {
        if (頭 == 0)
        {
            Total_HP = 0;
        }
        else if (胸 == 0)
        {

        }
        else if (臀 == 0)
        {

        }
        else if (左大 == 0)
        {
            
        }
        else if (左小 == 0)
        {
            
        }
        else if (右大 == 0)
        {
            
        }
        else if (右小 == 0)
        {
            
        }
    }
}
