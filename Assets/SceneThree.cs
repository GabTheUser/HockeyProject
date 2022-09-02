using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SceneThree : MonoBehaviour
{
    public float maxTimeToShoot;
    public bool startCountDown;
    public PhotonView pView;
    public GameObject[] opponents;
    public Transform marker;
    [SerializeField]
    private GameObject specShaiba;
    [SerializeField]
    private Transform[] positionsOfShaiba;

    // [HideInInspector]
    public float curTimeToShoot;
    private Amigo[] amigoScr = new Amigo[2];
    private AmigoShaiba amigoShaiba;
    private int randomPick;

    public bool toOff;

    private int tempShootOrPass;
    // Start is called before the first frame update
    void OnEnable()
    {
        amigoScr[0] = opponents[0].GetComponent<Amigo>();
        amigoScr[1] = opponents[1].GetComponent<Amigo>();
        amigoShaiba = specShaiba.GetComponent<AmigoShaiba>();
        amigoShaiba.scenarios = this;
        //Scene3();
        Scene3ResetVars();
        curTimeToShoot = maxTimeToShoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (pView.IsMine)
        {
            if (startCountDown)
            {
                if (curTimeToShoot > 0)
                {
                    curTimeToShoot -= Time.deltaTime;
                }
                else
                {
                    pView.RPC("TurnOffShaibaVisual", RpcTarget.All, true);

                    startCountDown = false;
                    pView.RPC("Scene3", RpcTarget.All);

                    //amigoScr[randomPick].Throw();
                }
            }
        }
    }

    [PunRPC]
    public void Scene3()
    {
        amigoScr[0].leading = false;
        amigoScr[1].leading = false;
        amigoShaiba.pointToGoTo = null;
        amigoShaiba.partner = null;
        amigoShaiba.tempAmigoScr = null;

        if (randomPick == 0)
        {
            amigoScr[0].leading = true;
            amigoScr[0].toShootOrPass = tempShootOrPass;
            if (amigoScr[0].toShootOrPass > 5) // pass
            {
                amigoScr[0].transform.LookAt(amigoScr[0].partner);
                amigoScr[0].Throw();
            }
            else
            {
                amigoScr[0].transform.LookAt(amigoScr[0].lookAtIt);
                amigoScr[0].Throw();
            }
            specShaiba.transform.position = positionsOfShaiba[0].position;
            
            //amigoScr[0].Throw();
        }
        else if (randomPick == 1)
        {
            amigoScr[1].leading = true;
            amigoScr[1].toShootOrPass = tempShootOrPass;
            if (amigoScr[1].toShootOrPass > 5)
            {
                amigoScr[1].transform.LookAt(amigoScr[1].partner);
                amigoScr[1].Throw();
            }
            else
            {
                amigoScr[1].transform.LookAt(amigoScr[1].lookAtIt);
                amigoScr[1].Throw();
            }
            specShaiba.transform.position = positionsOfShaiba[1].position;
           
            //amigoScr[1].Throw();
        }
        //throwB = true;
    }

    public void Scene3ResetVars()
    {
        if (pView.IsMine)
        {
            randomPick = Random.Range(0, 2);
            int shootPassRndO = Random.Range(0, 10);
            pView.RPC("Scene3ResetRPC", RpcTarget.All, randomPick, shootPassRndO);
        }
       
    }
    [PunRPC]
    public void Scene3ResetRPC(int randomNum, int shootPassRnd)
    {

        randomPick = randomNum;
        tempShootOrPass = shootPassRnd;
        if (randomPick == 0)
        {
            if (tempShootOrPass > 5)
            {
                marker.position = new Vector3(amigoScr[0].partner.position.x, marker.position.y, amigoScr[0].partner.position.z);
            }
            else
            {
                marker.position = new Vector3(amigoScr[0].transform.position.x, marker.position.y, amigoScr[0].transform.position.z);
            }
        }
        else
        {
            if (tempShootOrPass > 5)
            {
                marker.position = new Vector3(amigoScr[1].partner.position.x, marker.position.y, amigoScr[1].partner.position.z);
            }
            else
            {
                marker.position = new Vector3(amigoScr[1].transform.position.x, marker.position.y, amigoScr[1].transform.position.z);
            }
        }
        specShaiba.transform.position = positionsOfShaiba[randomPick].position;
        //  pView.RPC("TurnOffShaibaVisual", RpcTarget.All, true);
        
        curTimeToShoot = maxTimeToShoot;
        startCountDown = true;
    }

    [PunRPC]
    public void TurnOffShaibaVisual(bool activity)
    {
        toOff = activity;
        specShaiba.SetActive(toOff);
    }

    [PunRPC]
    public void PassPointu()
    {
        amigoShaiba.partner = amigoScr[randomPick].partner.gameObject;
        if (randomPick == 0)
        {
            amigoShaiba.tempAmigoScr = amigoScr[1].GetComponent<Amigo>();
        }
        else
        {
            amigoShaiba.tempAmigoScr = amigoScr[0].GetComponent<Amigo>();
        }
        amigoShaiba.pointToGoTo = amigoScr[randomPick].GetComponent<Amigo>().passPoint;


        /*}
        else
        {
            amigoShaiba.pointToGoTo = passPoint;
            //myPV.RPC("PassPoint", RpcTarget.All, 1);
        }


        amigoShaiba.partner = amigoScr.partner.gameObject;
        amigoShaiba.tempShootBall = amigoShaiba.partner.GetComponent<ShootBall>();
        amigoShaiba.pointToGoTo = amigoScr.passPoint;*/
    }
}
