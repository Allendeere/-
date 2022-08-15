using UnityEngine;
using Photon.Pun;


public class PlayerJoined : MonoBehaviour
{
    //[SerializeField] Camera cam;
    [SerializeField] PhotonView myPhotonView;
    [SerializeField] NetworkPlayer networkp;


    [SerializeField] private GameObject m_camera;



    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        if (myPhotonView.IsMine ==false)
        {
            Destroy(m_camera);
            networkp.enabled=false;
        }
    }


}
