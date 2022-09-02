using UnityEngine;
using TMPro;
using Photon.Pun;
public class PlaySound : MonoBehaviour
{
    public PhotonView weakSpotsPView;

    private AudioSource audioSource;

    private PhotonView pView;
    //private PhotonView myPView;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        pView = FindObjectOfType<ChangeScore>().gameObject.GetPhotonView();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {
            if (other.transform.root.GetComponent<Projectile>())
            {
                Projectile tempProj = other.transform.root.GetComponent<Projectile>();
                if (tempProj.hittedSomething == false)
                {
                    if (!weakSpotsPView.IsMine)
                    {
                        weakSpotsPView.RPC("MissedOneAdd", RpcTarget.All, other.transform.parent.position); // <
                    }
                    if (!pView.IsMine)
                    {
                        pView.RPC("EnemyScoreUp", RpcTarget.All);
                    }
                    tempProj.HitConfirmCaller();
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            if (collision.transform.root.GetComponent<Projectile>())
            {
                Projectile tempProj = collision.transform.root.GetComponent<Projectile>();
                if (tempProj.hittedSomething == false)
                {
                    tempProj.HitConfirmCaller();
                    if (!pView.IsMine)
                    {
                        pView.RPC("PlayerScoreUp", RpcTarget.All);
                    }
                    audioSource.PlayOneShot(audioSource.clip);
                }
            }
        }
    }

}
