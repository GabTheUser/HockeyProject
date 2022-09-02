using UnityEngine;
using Photon.Pun;
public class RPCcaller : MonoBehaviour
{
    public PhotonView pview;
    public OVRGrabbable grab;
    public bool calledKin, calledUnKin = true;

    private Projectile projectile;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        projectile = GetComponent<Projectile>();
    }
    private void Update()
    {
        if (calledKin == false)
        {
            if (grab.isGrabbed)
            {
                if (pview.IsMine)
                    pview.RPC("Kin", RpcTarget.All);
            }
        }
        if(calledUnKin == false)
        {
            if(grab.isGrabbed == false)
            {
                if (pview.IsMine)
                    pview.RPC("UnKin", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void Kin()
    {
        projectile.timerGo = false;
        rb.isKinematic = true;
        calledUnKin = false;
        calledKin = true;
    }

    [PunRPC]
    public void UnKin()
    {
        projectile.timerGo = true;
        rb.isKinematic = false;
        calledKin = false;
        calledUnKin = true;

    }
}
