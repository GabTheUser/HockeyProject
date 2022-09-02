using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amigo : MonoBehaviour
{
    public Transform specShaiba;
    public Transform partner;
    public Transform passPoint;
    public List<Transform> points;
    public GameObject ball;
    public Transform shootPoint;
    public float maxTimeToShoot;
    public SceneThree scenarios;
    float curTimeToShoot;
    public Animator animator;
    public AnimationClip animClip;
    public Transform lookAtIt;
    public bool leading;

    public int toShootOrPass;

    private AudioSource audioSource;
    public AmigoShaiba amigoShaiba;

    public PhotonView pViewSh;
    public PhotonView myPV;
    public PhotonView scenPV;
    // Start is called before the first frame update
    void Start()
    {
        lookAtIt = GameObject.Find("LookAtIt").transform;
        //points = FindObjectOfType<ShotPoints>().points;
        audioSource = GetComponent<AudioSource>();
    }

    public void Throw()
    {
        animator.Play(animClip.name);
    }

    public void ThrowBall()
    {
        //if (myPV.IsMine)
        {
            if (toShootOrPass <= 5)
            {
                transform.LookAt(lookAtIt);
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
              
                specShaiba.position = shootPoint.position;
                if(pViewSh.IsMine) pViewSh.RPC("TurnOffShaibaVisual", RpcTarget.All, false);
                if (!PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.Instantiate(ball.name, shootPoint.position, shootPoint.rotation);
                    //
                }
                if (scenarios != null)
                {
                    if (myPV.IsMine)
                    {
                        scenarios.Scene3ResetVars();
                    }
                }

            }
            else if (toShootOrPass > 5)
            {
                if (myPV.IsMine)
                {
                    myPV.RPC("PassPointu", RpcTarget.All);
                }
                shootPoint.LookAt(passPoint);
            }
            if(leading)
            leading = false;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    /*[PunRPC]
    public void PassPointu()
    {
        amigoShaiba.partner = partner.gameObject;
        if (partner.GetComponent<Amigo>())
        {
            //myPV.RPC("PassPoint", RpcTarget.All, 0);
            amigoShaiba.pointToGoTo = partner.GetComponent<Amigo>().shootPoint;
            amigoShaiba.tempAmigoScr = partner.GetComponent<Amigo>();
        }
        else
        {
            amigoShaiba.pointToGoTo = passPoint;
            //myPV.RPC("PassPoint", RpcTarget.All, 1);
        }
    }*/
}