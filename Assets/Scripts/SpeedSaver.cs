using UnityEngine;
using Photon.Pun;
public class SpeedSaver : MonoBehaviour
{
    [SerializeField]
    private Projectile projectile;
    public float speed;

    public PhotonView trainerPV;
    void Start()
    {
        speed = projectile.m_Speed;
    }
    [PunRPC]
    public void SyncValues(float nSpd)
    {
        speed = nSpd;
        if (projectile.m_Speed != speed)
        {
            projectile.m_Speed = speed;
        }
    }
}
