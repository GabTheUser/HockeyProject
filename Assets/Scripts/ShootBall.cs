using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ShootBall : MonoBehaviour
{
    public GameObject ball;
    public Transform shootPoint;
    public float maxTimeToShoot;
    public AnimationClip animClip;
    public Transform lookAtIt;
    public bool noTimeThrow;
    public Animator animator;
    public AudioSource audioSource;
    private AudioClip audioClip;
    private float curTimeToShoot;

    public PhotonView pView;
    void OnEnable()
    {
        lookAtIt = GameObject.Find("LookAtIt").transform;
        audioClip = audioSource.clip;
    }

    void Update()
    {
        if (noTimeThrow == false)
        {
            if (curTimeToShoot < maxTimeToShoot)
            {
                curTimeToShoot += Time.deltaTime;
            }
            else
            {
                curTimeToShoot = 0;
                Throw();
            }

            var lookPos = lookAtIt.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);
        }
    }

    public void Throw()
    {
        animator.Play(animClip.name);
    }

    public void ThrowBall()
    {
        if (lookAtIt == null)
        {
            lookAtIt = GameObject.Find("LookAtIt").transform;
        }
        var lookPos = lookAtIt.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);


        int rndSide = Random.Range(0, 2);
        float z;
        float y = Random.Range(-21.79f, -21.34f);
        float x = 15.082f;
        if (rndSide == 0) // right side attack
        {
            z = Random.Range(264.843f, 265.4f);
        }
        else // left side attack
        {
            z = Random.Range(265.85f, 266.412f);
        }
        shootPoint.LookAt(new Vector3(x, y, z));
        audioSource.PlayOneShot(audioClip);
        if (!PhotonNetwork.IsMasterClient)
        {
            GameObject ballTemp = PhotonNetwork.Instantiate(ball.name, shootPoint.position, shootPoint.rotation);
        }
        if (pView != null)
        {
            pView.RPC("TurnOffShaibaVisual", RpcTarget.All, false);
        }
    }

    public void RotateToLookAt()
    {
        if (lookAtIt == null)
        {
            lookAtIt = GameObject.Find("LookAtIt").transform;
        }
        var lookPos = lookAtIt.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 100);
    }
}
