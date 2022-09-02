using UnityEngine;
using TMPro;
using Photon.Pun;

public class SpeedMod : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed;
    [SerializeField] private TextMeshProUGUI textMPro;
    [SerializeField] PhotonView pView;


    private PhotonView spdSaver;
    private Projectile proj;
    private void Start()
    {
        spdSaver = FindObjectOfType<SpeedSaver>().gameObject.GetPhotonView();
        proj = projectile.GetComponent<Projectile>();
    }

    void OnEnable()
    {
        //textMPro.text = "" + projectile.GetComponent<Projectile>().m_Speed; 
        textMPro.text = "" + projectile.GetComponent<Projectile>().m_Speed;
        spdSaver = FindObjectOfType<SpeedSaver>().gameObject.GetPhotonView();
    }

    public void PlusSpeed()
    {
        if (proj.m_Speed < 7.6f) // 125
        {
            if (pView.IsMine)
            {
                proj.m_Speed += 0.1f;
                float newSpeed = proj.m_Speed;
                textMPro.text = "" + newSpeed;
                spdSaver.RPC("SyncValues", RpcTarget.AllBuffered, newSpeed);
            }
        }
        else
        {
            //+-5.5
            proj.m_Speed = 7.6f;
            float newSpeed = 7.6f;
            textMPro.text = "" + newSpeed;
            spdSaver.RPC("SyncValues", RpcTarget.AllBuffered, newSpeed);
        }
    }

    public void MinusSpeed()
    {
        if (proj.m_Speed > 6.6f) // 100 km
        {
            if (pView.IsMine) 
            { 
                proj.m_Speed -= 0.1f;
                float newSpeed = proj.m_Speed;
                textMPro.text = "" + newSpeed;
                spdSaver.RPC("SyncValues", RpcTarget.AllBuffered, newSpeed);
            }
        }
        else
        {
            proj.m_Speed = 6.6f;
            float newSpeed = 6.6f;
            textMPro.text = "" + newSpeed;
            spdSaver.RPC("SyncValues", RpcTarget.AllBuffered, newSpeed);
        }
    }

    /*public void PlusSpeed()
    {
        if (spdSaver.IsMine)
        {
            spdSaver.RPC("PlusSpeedRPC", RpcTarget.AllBuffered);
            spdSaver.RPC("SyncValues", RpcTarget.AllBuffered, newSpeed);
        }
    }

    public void MinusSpeed()
    {
        if (spdSaver.IsMine)
        {
            spdSaver.RPC("MinusSpeedRPC", RpcTarget.AllBuffered);
            spdSaver.RPC("SyncValues", RpcTarget.AllBuffered, newSpeed);
        }
    }

    [PunRPC]
    public void MinusSpeedRPC()
    {
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj.m_Speed > 700)
        {
            proj.m_Speed -= 43.75f;
            float newSpeed = proj.m_Speed;
            textMPro.text = "" + newSpeed / 8.75f;
        }
    }

    [PunRPC]
    public void PlusSpeedRPC()
    {
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj.m_Speed < 1050)
        {
            proj.m_Speed += 43.75f;
            newSpeed = proj.m_Speed;
            textMPro.text = "" + newSpeed / 8.75f;

        }
    }*/


    /*[PunRPC]
    public void SyncValues(float myFloat)
    {
        projectile.GetComponent<Projectile>().m_Speed = myFloat;
    }*/
}