using UnityEngine;
using Photon.Pun;
public class CentrCameraVars : MonoBehaviour
{
    public Vector3 camPosition;
    public Quaternion camRotation;
    public GameObject trainerCam;


    [PunRPC]
    public void CopyCamVar(Vector3 position, Quaternion rotation)
    {
        camPosition = position;
        camRotation = rotation;

        if (trainerCam != null)
        {
            trainerCam.transform.SetPositionAndRotation(camPosition, camRotation);
        }
    }
}
