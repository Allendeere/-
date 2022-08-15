using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.Unity;
using Photon.Voice.PUN;


public class VoiceChat : MonoBehaviourPun
{

    public KeyCode PushButton = KeyCode.P;
    public Recorder VoiceRecorder;
    private PhotonView view;
    

    void Start()
    {
        view = photonView;
        VoiceRecorder.TransmitEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKeyDown(PushButton))
            {


                VoiceRecorder.TransmitEnabled = true;

            }

            else if (Input.GetKeyUp(PushButton))
            {

                VoiceRecorder.TransmitEnabled = false;

            }
        }
    }

    public void Mic_On() { VoiceRecorder.TransmitEnabled = true; }

    public void Mic_Off() { VoiceRecorder.TransmitEnabled = false; }

}
