using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    private Transform[] spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            FindObjectOfType<CentrCameraVars>().trainerCam = PhotonNetwork.Instantiate(players[0].name, spawnPoint[0].position, spawnPoint[0].rotation);
        }
        else
        {
            PhotonNetwork.Instantiate(players[1].name, spawnPoint[1].position, spawnPoint[1].rotation);
        }
    }
}
