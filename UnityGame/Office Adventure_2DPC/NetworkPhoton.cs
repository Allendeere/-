using Photon.Pun;
using UnityEngine;

namespace UnderdogCity
{

    public class NetworkPhoton : MonoBehaviourPun, IPunObservable
    {
        [SerializeField] PhotonView myPhotonView;

        protected playermovement Player;
        protected Vector3 RemotePlayerPosition;
        //protected float RemoteLookX;
      // protected float LookXVel;

        private void Start()
        {
            myPhotonView = GetComponent<PhotonView>();
            Player = GetComponent<playermovement>();

            //destroy the controller if the player is not controlled by me
            if (myPhotonView.IsMine == false)
            {
                Destroy(GetComponent<CharacterController2D>());
                Destroy(GetComponent<playermovement>());
            }
            
        }

        public void Update()
        {
            if (photonView.IsMine)
                return;

            var LagDistance = RemotePlayerPosition - transform.position;

            //High distance => sync is to much off => send to position
            if (LagDistance.magnitude >5)
            {
                transform.position = RemotePlayerPosition;
                LagDistance = Vector3.zero;
            }

            //ignore the y distance
            LagDistance.y = 0;

            if (LagDistance.magnitude < 0.1f)
            {
                //Player is nearly at the point
                Player.horizontalMove = 0;
            }
            else
            {
                //Player has to go to the point
                Player.horizontalMove = LagDistance.normalized.x;
            }

            //jump if the remote player is higher than the player on the current client
            Player.jump = RemotePlayerPosition.y - transform.position.y > 0.2f;

            //Look Smooth
     //       Player.Input.LookX = Mathf.SmoothDamp(Player.Input.LookX, RemoteLookX, ref LookXVel, 0.2f);

        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(RemotePlayerPosition, 1f);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
             //   stream.SendNext(Player.Input.LookX);
            }
            else
            {
                RemotePlayerPosition = (Vector3)stream.ReceiveNext();
                //RemoteLookX = (float)stream.ReceiveNext();

            }
        }
    }
}