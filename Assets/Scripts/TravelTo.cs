using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TravelTo : MonoBehaviour
{
    [SerializeField]
    private Transform[] tpPoints;
    [SerializeField]
    private float opSpeed;
    [SerializeField]
    private Transform whoToLookAt;
    [SerializeField]
    private Amigo amigoScr;
    [SerializeField]
    private AmigoShaiba amigoShaiba;
    public int counter;

    public BallOwner ballOwner;
    public bool passedBall;
    public bool lookAtPartner;
    public PhotonView pView, minePV;
    [Header("for leading one")]
    public MeshRenderer meshu;
    private void Update()
    {
        if (opSpeed != 0)
        {
            if (transform.position == tpPoints[counter].position)
            {
                if (counter < tpPoints.Length - 1)
                {
                    counter++;
                }
                else
                {
                    if (ballOwner != null)
                    {
                        if (passedBall == false)
                        {
                            transform.LookAt(amigoShaiba.partner.transform);
                            amigoScr.partner = amigoShaiba.partner.transform;
                            amigoScr.passPoint = ballOwner.whereToPass;
                            amigoShaiba.amigoTravelTo = amigoScr.partner.GetComponent<TravelTo>();
                            amigoScr.toShootOrPass = 8;
                            passedBall = true;
                            amigoScr.Throw();
                            ballOwner.dontHoldBall = true;
                            if (meshu != null)
                            {
                                meshu.enabled = false;
                            }
                            //amigoScr.Throw();
                            //ballOwner.dontHoldBall = true;
                            //minePV.RPC("MakePartner", RpcTarget.All);
                            //pView.RPC("TurnOffShaibaVisual", RpcTarget.All, true);
                            // amigoShaiba.pointToGoTo = ballOwner.whereToPass;
                        }

                    }
                    return;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, tpPoints[counter].position, opSpeed * Time.deltaTime);
            }
        }
        if (lookAtPartner)
        {
            transform.LookAt(whoToLookAt);
        }
    }
    /*public void WhereToPointPass()
    {
       amigoShaiba.pointToGoTo = ballOwner.whereToPass;
   }

    [PunRPC]
    public void MakePartner()
    {
        if (!PhotonNetwork.IsMasterClient) Debug.LogError("Client runs makePartner");
        amigoScr.Throw();
        ballOwner.dontHoldBall = true;
    }*/
}
