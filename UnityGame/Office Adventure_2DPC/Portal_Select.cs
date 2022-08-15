using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Select : MonoBehaviour
{
    [SerializeField] GameObject[] Page;
    [SerializeField] GameObject[] LevelPanels;
    [SerializeField] Near_Click[] nearClicks; // 3 
    [SerializeField] int page=1;
    [SerializeField] int Max_page = 2;
    [SerializeField] int[] level= {1,2,3} ;

    private void Start()
    {
        Max_page = LevelPanels.Length / 3;

        page= PlayerPrefs.GetInt("TemporaryStorage_PortalLevel", 1);
        for (int i = 0; i < level.Length; i++)
        {
            level[i] += 3*(page-1);
            nearClicks[i].OpenOBJ = LevelPanels[level[i] - 1];
        }
        for (int i = 0; i < Page.Length; i++)
        {
            Page[i].SetActive(false);
            if (i <= page)
            {
                Page[i].SetActive(true);
            }
        }
    }
    public void ButtonClick(bool up)
    {
        if (up && page > 1)
        {
            for (int i = 0; i < level.Length; i++)
            {
                level[i] -= 3;
                nearClicks[i].OpenOBJ = LevelPanels[level[i] - 1];
            }
            page--; 
        }
        else if(!up)
        {
            if (Max_page > page)
            {
                for (int i = 0; i < level.Length; i++)
                {
                    level[i] += 3;
                    nearClicks[i].OpenOBJ = LevelPanels[level[i] - 1];
                }
                page++; 
            }
        }
        else
        {
            Debug.Log("已經到底部了");
        }
       
        for (int i = 0; i < Page.Length; i++)
        {
            Page[i].SetActive(false);
            if(i<= page)
            {
                Page[i].SetActive(true);
            }
        }

        PlayerPrefs.SetInt("TemporaryStorage_PortalLevel", page);
    }
}
