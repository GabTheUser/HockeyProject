using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
public class SceneSpecMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int myNum;
    [SerializeField]
    private GameObject mySceneObj;
    [SerializeField]
    private Vector3 posSpawn;
    [SerializeField]
    private GameObject menuUI;
    [SerializeField]
    private Color onColor, offColor;

    private bool currentState;
    SceneFourController sceneFourControl;
    private bool clickedu;
    private void OnEnable()
    {
        sceneFourControl = FindObjectOfType<SceneFourController>();
        if (!GetComponent<Button>())
        {
            GameObject myChar = mySceneObj.GetComponent<Scenarios>().characterToOn[myNum];
            //clickedu = false;
            myChar.SetActive(currentState);
            if (clickedu == false)
            {
                GetComponent<Image>().color = offColor;
                //if (sceneFourControl != null)
                    sceneFourControl.GetComponent<PhotonView>().RPC("RemoveMyInt", RpcTarget.All, myNum);

            }
            else
            {
                GetComponent<Image>().color = onColor;
                //if (sceneFourControl != null)
                    sceneFourControl.GetComponent<PhotonView>().RPC("GetMyInt", RpcTarget.All, myNum);
            }
        }
    }
    private void SetActiveCharacter()
    {
        //GameObject myChar = mySceneObj.GetComponent<Scenarios>().characterToOn[myNum];
        //clickedu = myChar.activeSelf;
        //currentState = !currentState;
        //myChar.SetActive(currentState);
        if (clickedu == false)
        {
            GetComponent<Image>().color = offColor;
                sceneFourControl.GetComponent<PhotonView>().RPC("RemoveMyInt", RpcTarget.All, myNum);
        }
        else
        {
            GetComponent<Image>().color = onColor;
                sceneFourControl.GetComponent<PhotonView>().RPC("GetMyInt", RpcTarget.All, myNum);
        }
    }

    public void RunGame()
    {
        GameObject newScn4 = PhotonNetwork.Instantiate(mySceneObj.name, posSpawn, Quaternion.identity);
            sceneFourControl.GetComponent<PhotonView>().RPC("ActivateChars4", RpcTarget.All);
        menuUI.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickedu = !clickedu;
        SetActiveCharacter();
    }
}
