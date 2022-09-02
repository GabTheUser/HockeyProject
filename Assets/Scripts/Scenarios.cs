using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Scenarios : MonoBehaviour
{
    [SerializeField]
    private UnityEvent sceneEvent;
    public int sceneNum;
    public float maxTimeToTP;
    private float timeToTP;
    public Transform[] tpPoints;
    public GameObject[] opponents;
    public float opSpeed;
    public int counter;
    [Space(2)]
    [Header("Scene 4")]
    public GameObject[] characterToOn;
    [SerializeField]
    private GameObject scene4CharacterHolder;
    private int countCharacter;
    private List<GameObject> currentOponents = new List<GameObject>();
    public PhotonView scObjPView;
    public PhotonView pView;
    // [HideInInspector]
    public Transform marker;
   

    private int randomPick;
    private void OnEnable()
    {
        if (sceneNum == 4)
        {
            for (int i = 0; i < characterToOn.Length; i++)
            {
                if (characterToOn[i].activeSelf == true)
                {
                    countCharacter++;
                    currentOponents.Add(characterToOn[i]);
                }
            }
        }
    }
    public void Update()
    {
        if (pView.IsMine)
        {
            sceneEvent?.Invoke();
        }
    }

    public void Scene1()
    {
        if (timeToTP < maxTimeToTP + 0.5f)
        {
            timeToTP += Time.deltaTime;
        }
        else
        {
            timeToTP = 0;
            int rnd = Random.Range(0, tpPoints.Length - 1);
            opponents[0].transform.position = tpPoints[rnd].position;
            if (opponents[0] == null) Destroy(gameObject);
        }
    }

    public void Scene2()
    {
        opponents[0].transform.position = Vector3.MoveTowards(opponents[0].transform.position, tpPoints[counter].position, opSpeed * Time.deltaTime);
        if (opponents[0].transform.position == tpPoints[counter].position)
        {
            if (counter < tpPoints.Length - 1)
                counter++;
            else
            {
                counter = 0;
            }
        }
    } 

    public void Scene4()
    {
        if (timeToTP <= 0)
        {
            randomPick = Random.Range(0, countCharacter);
            pView.RPC("ChangeMarkerLoc", RpcTarget.All, randomPick);
        }
        if (timeToTP < maxTimeToTP + 0.5f)
        {
            timeToTP += Time.deltaTime;
        }
        else
        {
            timeToTP = 0;
            pView.RPC("Control4Scene", RpcTarget.Others, randomPick);
        }
    }

    [PunRPC]
    public void Control4Scene(int randomNum)
    {
        randomPick = randomNum;
        ShootBall myShootBall = currentOponents[randomNum].GetComponent<ShootBall>();
        myShootBall.RotateToLookAt();
        myShootBall.Throw();
    }
    [PunRPC]
    public void ChangeMarkerLoc(int randomNum)
    {
        randomPick = randomNum;
        marker.position = new Vector3(currentOponents[randomPick].transform.position.x, marker.position.y, currentOponents[randomPick].transform.position.z);
    }

    [PunRPC]
    public void Scene4Actions(int charNum, bool activated)
    {
        characterToOn[charNum].SetActive(activated);
    }
}
