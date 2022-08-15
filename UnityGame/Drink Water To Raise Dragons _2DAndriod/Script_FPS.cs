using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Script_FPS : MonoBehaviour
{
    private int lastRequestedFrame = 0;
    private void Awake()
    {
        Application.targetFrameRate = 12;
        Notifications.SendMessage();
    }

    public void RequestFullFrameRate()
    {
        lastRequestedFrame = Time.frameCount;
    }

    private void Update()
    {
        if (Time.deltaTime > 300)
        {
            OnDemandRendering.renderFrameInterval = (Time.frameCount - lastRequestedFrame) < 3 ? 1 : 12;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
   
}
