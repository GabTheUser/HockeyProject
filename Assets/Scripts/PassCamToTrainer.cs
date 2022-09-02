using UnityEngine;
using Photon.Pun;
public class PassCamToTrainer : MonoBehaviour
{
    public GameObject vrCam;
    private PhotonView pView;
    private PhotonView myPView;

    public GameObject trainerCam;
    private void Start()
    {
        myPView = GetComponent<PhotonView>();
        if (myPView.IsMine)
            pView = FindObjectOfType<CentrCameraVars>().gameObject.GetPhotonView();
        else GetComponent<PassCamToTrainer>().enabled = false;
        //FindObjectOfType<CopyVrCam>().vrCam.transform.parent = vrCam.transform;
    }
    /*void Start()
    {
        if(GetComponent<PhotonView>().IsMine)
        GetComponent<PhotonView>().RPC("PassCam", RpcTarget.All);
    }
    
    [PunRPC]
    public void PassCam()
    {
        FindObjectOfType<CopyVrCam>().vrCam = vrCam;
    }*/

    private void Update()
    {
        //if (!pView.IsMine)
        pView.RPC("CopyCamVar", RpcTarget.MasterClient, vrCam.transform.position, vrCam.transform.rotation);
    }

    
}
