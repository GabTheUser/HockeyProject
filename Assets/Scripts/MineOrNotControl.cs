using UnityEngine;
using Photon.Pun;
public class MineOrNotControl : MonoBehaviour
{
    private PhotonView pView;
    [SerializeField]
    private GameObject myCam;
    private void Start()
    {
        pView = GetComponent<PhotonView>();
        if (!pView.IsMine)
        {
            myCam.GetComponent<Camera>().enabled = false;
            if (GetComponent<OVRCameraRig>())
            {
                GetComponent<OVRCameraRig>().enabled = false;
                GetComponent<OVRManager>().enabled = false;
                GetComponent<OVRHeadsetEmulator>().enabled = false;
            }
        }
    }
}
