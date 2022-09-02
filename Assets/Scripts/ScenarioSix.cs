using UnityEngine;
using Photon.Pun;
public class ScenarioSix : MonoBehaviour
{
    [SerializeField]
    private TravelTo[] travelTo;
    [SerializeField]
    private Transform[] npcs;
    [SerializeField]
    private Transform[] npcWhoTakes;
    [SerializeField]
    private Transform[] passPoints;
    [SerializeField]
    private AmigoShaiba amigoShaiba;
    [SerializeField]
    private BallOwner ballOwner;
    [SerializeField]
    private Transform[] startingPositions;
    [SerializeField]
    private Amigo amigoScr;
    [SerializeField]
    private TravelTo mainTravelTo;
    [SerializeField]
    private Transform marker;
    [SerializeField]
    private Transform[] markerFollows;

    public float maxTimeToShoot;
    [SerializeField]
    private float[] speeds;
    //[HideInInspector]
    public float curTimeToShoot;
    //[HideInInspector]
    public bool startCountDown;

    private PhotonView myPV;

    public GameObject shaiba;
    public bool toOff;

    private int randomInt;
    void Start()
    {
        myPV = GetComponent<PhotonView>();
        /*for (int i = 0; i < startingPositions.Length; i++)
        {
            startingPositions[i] = npcs[i];
        }*/
        //if(myPV.IsMine)
        ActionReStart();
        //myPV.RPC("ActionReStart", RpcTarget.All);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // myPV.RPC("ActionReStart", RpcTarget.All);
            ActionReStart();

        }
        if (myPV.IsMine)
        {
            if (startCountDown)
            {
                if (curTimeToShoot > 0)
                {
                    curTimeToShoot -= Time.deltaTime;
                }
                else
                {
                    //pView.RPC("TurnOffShaibaVisual", RpcTarget.All, true);
                    ActionReStart();
                    startCountDown = false;
                }
            }
        }
    }

    //[PunRPC]
    public void ActionReStart()
    {
        /*amigoShaiba.partner = null;
        amigoShaiba.pointToGoTo = null;
        amigoScr.passPoint = null;
        mainTravelTo.passedBall = false;
        ballOwner.dontHoldBall = false;
        marker.GetComponent<BallOwner>().whoToFollow = null;*/
        if (myPV.IsMine)
        {
            int rnd = Random.Range(0, npcWhoTakes.Length);
            if (myPV.IsMine)
            {
                myPV.RPC("PickRandomNum", RpcTarget.All, rnd);

                myPV.RPC("TurnOffShaibaVisual", RpcTarget.All, true);
            }
        }

        /*amigoShaiba.partner = npcWhoTakes[randomInt].gameObject;
        amigoShaiba.speed = speeds[randomInt];
        ballOwner.whereToPass = passPoints[randomInt];
        marker.GetComponent<BallOwner>().whoToFollow = markerFollows[randomInt];*/

       
    }

    [PunRPC]
    public void PickRandomNum(int rndom)
    {
        for (int i = 0; i < startingPositions.Length; i++)
        {
            npcs[i].position = startingPositions[i].position;
        }
        amigoShaiba.partner = null;
        amigoShaiba.pointToGoTo = null;
        amigoScr.passPoint = null;
        mainTravelTo.passedBall = false;
        ballOwner.dontHoldBall = false;
        marker.GetComponent<BallOwner>().whoToFollow = null;
        randomInt = rndom;
        amigoShaiba.partner = npcWhoTakes[randomInt].gameObject;
        amigoShaiba.speed = speeds[randomInt];
        ballOwner.whereToPass = passPoints[randomInt];
        marker.GetComponent<BallOwner>().whoToFollow = markerFollows[randomInt];

    }
    [PunRPC]
    public void PassPointu()
    {
        amigoShaiba.partner = amigoScr.partner.gameObject;
        amigoShaiba.tempShootBall = amigoShaiba.partner.GetComponent<ShootBall>();
        amigoShaiba.pointToGoTo = amigoScr.passPoint;
        //myPV.RPC("PassPoint", RpcTarget.All, 1);
    }

    [PunRPC]
    public void TurnOffShaibaVisual(bool activity)
    {
        toOff = activity;
        shaiba.SetActive(toOff);
    }

}
