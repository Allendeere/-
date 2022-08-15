using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseUI;

    [Header("麥克風")]
    [SerializeField] bool Open_Mic;
    [SerializeField] Sprite Mic_on;
    [SerializeField] Sprite Mic_off;
    [SerializeField] Image micbutton;

    bool swb;


    private void Start()
    {
        LoadMenu();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                Resume(pauseUI);
            }
            else
            {
                Pause(pauseUI);
            }
        }
    }
    public void Resume(GameObject plan)
    {
        plan.SetActive(false);
        //Time.timeScale = 1f;
        IsPaused = false;
    }
    public void Pause(GameObject plan)
    {
        plan.SetActive(true);
        //Time.timeScale = 0;
        IsPaused = true;
    }

    public void LoadMenu()
    {
        //Time.timeScale = 1;
        IsPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //-----------------------------------麥克風------------------------------------
    public void Mic()
    {
        Open_Mic = !Open_Mic;

        
        VoiceChat vc = GameObject.FindGameObjectWithTag("Player").GetComponent<VoiceChat>();
        if (Open_Mic)
        {
            vc.Mic_On();
            micbutton.sprite = Mic_on;

        }
        else 
        {
            vc.Mic_Off();
            micbutton.sprite = Mic_off;
        }
    }

    public void Sw(GameObject sw)
    {
        swb = !swb;
        if (swb)
        {
            sw.SetActive(true);
        }
        else
        {
            sw.SetActive(false);
        }
        
    }
}
