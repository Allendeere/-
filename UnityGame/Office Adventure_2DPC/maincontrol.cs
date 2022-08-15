using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;

public class maincontrol : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown resolutionDropdown;
    int currentResolutionIndex = 0;
    Resolution[] resolutions;
    public Conscience_count _count;
    [SerializeField] Notes notes;
    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

        }
        if (!File.Exists(Application.dataPath + "/Save.txt"))
        {
            Debug.Log("jj");
            FileStream fs = new FileStream(Application.dataPath + "/Save.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(1);
            sw.Close();
            fs.Close();
        }
        else
        {
            FileStream fs = new FileStream(Application.dataPath + "/Save.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            currentResolutionIndex = int.Parse(sr.ReadLine());
            sr.Close();
            fs.Close();
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        FileStream fs = new FileStream(Application.dataPath + "/Save.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        sw.WriteLine(resolutionIndex);
        sw.Close();
        fs.Close();
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void QuitGame()
    {
        Debug.Log("離開遊戲");
        Application.Quit();
    }

    public void ReSetGame()
    {
        Debug.Log("重設遊戲");
        PlayerPrefs.SetInt("levelReached", 0);
        PlayerPrefs.SetInt("Mission_Progress", 0);
        PlayerPrefs.SetString("mission", null);
        PlayerPrefs.SetInt("TemporaryStorage_PortalLevel", 0);
        //清除節奏
        for (int i = 0; i < notes.MyRhythm.Count; i++)
        {
            if (i % 2 == 0) { PlayerPrefs.SetInt("note_" + i, 5); }
            else { PlayerPrefs.SetInt("note_" + i, 0); }
           
        }


        _count.Conscience = 0;
        _count.Kubo_Kill = 0;  _count.Kubo_Tame = 0;
        _count.BubblePing_Kill = 0; _count.BubblePing_Tame = 0;
        _count.BubblePing_Soul =false; _count.Kubo_Soul = false;
    }
}
