using Photon.Pun;
using UnityEngine;
public class ExitScript : MonoBehaviour
{
    public void ExitApp()
    {
        Debug.LogError("exit");
        Application.Quit();
    }

    public void GoToMenu()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(1);
    }
}
