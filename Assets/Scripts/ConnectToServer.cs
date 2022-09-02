using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private UnityEvent reConnect;
    [SerializeField]
    private TextMeshProUGUI textOnScreen;

    [SerializeField]
    private GameObject whatToDo;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

        if (PhotonNetwork.IsConnected == false)
        {
            whatToDo.SetActive(true);
            textOnScreen.gameObject.SetActive(true);
            textOnScreen.text = "Connection failed";
        }
    }
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.B))
        {
            reConnect?.Invoke();
        }

        if (PhotonNetwork.IsConnected == false)
        {
            whatToDo.SetActive(true);
            textOnScreen.text = "Connection failed";
            textOnScreen.gameObject.SetActive(true);
        }
        else
        {
            whatToDo.SetActive(false);
            textOnScreen.gameObject.SetActive(true);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void TryConnectMasterAgain()
    {
        whatToDo.SetActive(false);
        textOnScreen.text = "Reconnecting";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ReconnectToMaster()
    {
        whatToDo.SetActive(false);
        textOnScreen.text = "Reconnecting";
        PhotonNetwork.Reconnect();
    }
}
