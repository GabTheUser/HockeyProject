using UnityEngine;
using Photon.Pun;
public class TurnOfIfNotMine : MonoBehaviour
{
    private void Start()
    {
        if (!GetComponent<PhotonView>().IsMine) gameObject.SetActive(false);        
    }
}
