using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    [SerializeField] Animator an;

    public void TestCreateroom()
    {
        if (createInput.text == "") { PhotonNetwork.CreateRoom("Room" + (Random.Range(1, 99999))) ; }
        else { CreatRoom(); }
    }

    
    public void CreatRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void TestJoinroom()
    {
        if(joinInput.text == "") { an.SetTrigger("Trigger"); }
        else { JoinRoom(); }
    }
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(" N O N O ³á !");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        an.SetTrigger("Trigger");
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("menu");
    }
}
