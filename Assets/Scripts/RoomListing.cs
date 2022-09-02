using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
public class RoomListing : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textName;

    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        textName.text = /*roomInfo.MaxPlayers + ", "*/"" + roomInfo.Name;  
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(textName.text);
    }
}
