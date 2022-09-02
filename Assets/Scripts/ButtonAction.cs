using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
public class ButtonAction : MonoBehaviour
{
    [SerializeField] private GameObject scenarios;
    [SerializeField] private GameObject sceneMenu;
    [SerializeField] private GameObject exitButton;
    [Space]
    [SerializeField] private GameObject goMenuButton;
    public bool Menutton;

    [SerializeField]
    private string poSpawnerName;

    private Vector3 posSpawn; // for 4

    //[SerializeField]
    //private string gameObjName;
    [SerializeField]
    private int sceneNum;
    [SerializeField]
    private GameObject sceneSpecMenu;

    private PhotonView pView;
    private void Start()
    {
        if (sceneNum == 4)
        {
            sceneSpecMenu.SetActive(false);
        }
        posSpawn = GameObject.Find(poSpawnerName).transform.position;
        pView = FindObjectOfType<ChangeScore>().gameObject.GetPhotonView();
        // scenarios = FindObjectOfType<ScenarioList>().scenarios[gameObjName];

    }
    public void Scenario1()
    {
        if (sceneNum == 4)
        {
            //GameObject curScene = PhotonNetwork.Instantiate(scenarios.name, posSpawn2, Quaternion.identity);
            //curScene.GetComponent<Scenarios>().enabled=false;
            goMenuButton.GetComponent<ButtonAction>().scenarios = scenarios;
            goMenuButton.SetActive(true);
            sceneSpecMenu.SetActive(true);
            sceneMenu.SetActive(false);
        }
        else
        {
            PhotonNetwork.Instantiate(scenarios.name, posSpawn, Quaternion.identity);
            //scenarios.SetActive(true);
            sceneMenu.SetActive(false);
            //exitButton.SetActive(false);
            goMenuButton.GetComponent<ButtonAction>().scenarios = scenarios;
            goMenuButton.SetActive(true);
        }
        pView.RPC("ScoreToZero", RpcTarget.All);
    }

    public void GoMenu()
    {
        if (sceneSpecMenu!=null)
        sceneSpecMenu.SetActive(false);
        if (FindObjectOfType<Scenarios>())
        {
            PhotonNetwork.Destroy(FindObjectOfType<Scenarios>().gameObject);
            //PhotonNetwork.Destroy(goMenuButton.GetComponent<ButtonAction>().scenarios);
        }
        //scenarios.SetActive(false);
        //sceneMenu.SetActive(true);
        //exitButton.SetActive(true);
        GetComponentInParent<UiButtonsActions>().ShowWeakSpots();
        if (Menutton == true)
        scenarios = null;
        goMenuButton.SetActive(false);
    }
}
