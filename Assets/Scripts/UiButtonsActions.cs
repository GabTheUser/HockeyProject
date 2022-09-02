using UnityEngine;
using Photon.Pun;
public class UiButtonsActions : MonoBehaviour
{
    [SerializeField]
    private GameObject menuObject;
    [SerializeField]
    private GameObject weakSpotDis;
    [SerializeField]
    private GameObject toMenuButton;
    private WeakSpots weakSpots;

    private PhotonView pViewGates;

    private void Start()
    {
        weakSpotDis = GameObject.Find("Weak Spots UI").GetComponent<Container>().savedObject;
        weakSpots = GameObject.Find("Gates").GetComponent<WeakSpots>();
        pViewGates = weakSpots.gameObject.GetComponent<PhotonView>();
    }

    public void ShowWeakSpots()
    {
        pViewGates.RPC("ActivateWSObj",RpcTarget.All, true);
        //weakSpotDis.SetActive(true);
        menuObject.SetActive(false);
        toMenuButton.SetActive(true);
    }

    public void HideWeakSpots()
    {
        pViewGates.RPC("ActivateWSObj", RpcTarget.All, false);
        //weakSpotDis.SetActive(false);
        menuObject.SetActive(true);
        toMenuButton.SetActive(false);
        //weakSpots.ClearWeakSpots();
        pViewGates.RPC("ClearWeakSpots", RpcTarget.All);
    }

    
}
