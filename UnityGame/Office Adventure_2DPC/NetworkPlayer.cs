using Photon.Pun;
using UnityEngine;

public class NetworkPlayer : MonoBehaviourPun, IPunObservable
{
    protected playermovement player;
    protected Vector3 RemotePlayerPosition;
    private void Awake()
    {
        player = GetComponent<playermovement>();
        
    }
    private void Update()
    {
        if (photonView.IsMine)
            return;

        var LagDistane = RemotePlayerPosition - transform.position;
        if(LagDistane.magnitude > 5f)
        {
            transform.position = RemotePlayerPosition;
            LagDistane = Vector3.zero;
        }

        LagDistane.y = 0;

        if(LagDistane.magnitude < 0.11f)
        {
            player.horizontalMove = 0;
        }
        else
        {
            player.horizontalMove = LagDistane.normalized.x; 
        }

        player.jump = RemotePlayerPosition.y - transform.position.y > 0.2f;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) // �� �C �� �� �a �� �� �W �� �� �m
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);        // �N �� �a �y �� �g �J �� ��
        }
        else
        {
            RemotePlayerPosition = (Vector3)stream.ReceiveNext(); // �� �� �� �y �� �� �N �� �{ �� �a 
        }                                                         // �� �y �� �s �J RemotePlayerPosition
    }
}
