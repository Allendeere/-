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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) // 序 列 化 玩 家 屏 幕 上 的 位 置
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);        // 將 本 地 座 標 寫 入 網 絡
        }
        else
        {
            RemotePlayerPosition = (Vector3)stream.ReceiveNext(); // 接 收 到 座 標 時 將 遠 程 玩 家 
        }                                                         // 的 座 標 存 入 RemotePlayerPosition
    }
}
