using Photon.Pun;
using UnityEngine;

public class AmigoShaiba : MonoBehaviour
{
    public GameObject shaibaObjectToOff;
    public Transform pointToGoTo;
    public GameObject partner;
    public Amigo tempAmigoScr;
    public ShootBall tempShootBall;
    public float speed;
    public SceneThree scenarios;
    [SerializeField]
    private ScenarioSix scenarioSix;
    public TravelTo amigoTravelTo;
    private Transform lookAt;

    public PhotonView myPV;
    private bool toOff;

    private Quaternion exRotation;
    private void Start()
    {
        //if(myPV.IsMine)
        lookAt = GameObject.Find("LookAtIt").transform;
    }
    void Update()
    {
        if (pointToGoTo != null)
        {
            if (transform.position != pointToGoTo.position)
            {
                //if(PhotonNetwork.IsMasterClient)
                transform.position = Vector3.MoveTowards(transform.position, pointToGoTo.position, speed * Time.deltaTime);
               // if (myPV.IsMine)
                    //myPV.RPC("TurnOffShaibaVisual", RpcTarget.All, true);
            }
            else if(Mathf.Approximately(transform.position.x, pointToGoTo.position.x) && Mathf.Approximately(transform.position.z, pointToGoTo.position.z))
            {
                if (tempAmigoScr)
                {
                    RotatePartner();
                    if (partner.transform.rotation == exRotation)
                    {
                        tempAmigoScr.toShootOrPass = 1;
                        tempAmigoScr.Throw();
                        //partner.GetComponent<Animator>().Play(partner.GetComponent<Amigo>().animClip.name);
                        pointToGoTo = null;
                        partner = null;
                        tempAmigoScr = null;
                        //if (myPV.IsMine) myPV.RPC("TurnOffShaibaVisual", RpcTarget.All, false);
                        return;
                    }
                    if (partner != null)
                        exRotation = partner.transform.rotation;
                }
                else if (tempShootBall)
                {
                    amigoTravelTo.lookAtPartner = false;
                    RotatePartner();
                    if (partner.transform.rotation == exRotation)
                    {
                        partner.transform.LookAt(lookAt);
                        tempShootBall.Throw();
                        //partner.GetComponent<Animator>().Play(partner.GetComponent<ShootBall>().animClip.name);
                        scenarioSix.curTimeToShoot = scenarioSix.maxTimeToShoot;
                        scenarioSix.startCountDown = true;
                        //Debug.LogError(scenarioSix.startCountDown);
                        pointToGoTo = null;
                        partner = null;
                        amigoTravelTo.lookAtPartner = true;
                        tempShootBall = null;
                        //if (myPV.IsMine) myPV.RPC("TurnOffShaibaVisual", RpcTarget.All, false);
                        return;
                    }
                    if (partner != null)
                        exRotation = partner.transform.rotation;
                }
                //pointToGoTo = null;
                //partner = null;
            }
        }

    }

   
    public void RotatePartner()
    {
        //if (!PhotonNetwork.IsMasterClient)
        {
            var lookPos = partner.transform.position - lookAt.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(-lookPos);
            partner.transform.rotation = Quaternion.Slerp(partner.transform.rotation, rotation, Time.deltaTime * 40);
        }
        //partner.transform.rotation = Quaternion.RotateTowards(partner.transform.rotation, rotation, Time.deltaTime * 2);
    }
}
