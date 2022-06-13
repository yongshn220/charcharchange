using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class cshRoomData : MonoBehaviour
{

    public Transform buttom;
    public RoomInfo _roomInfo;

    public TMP_Text roomInfoText;

    public RoomInfo RoomInfo
    {
        get
        {
            return _roomInfo;
        }

        set
        {
            this._roomInfo = value;
            roomInfoText.text = $"{this._roomInfo.Name}  ({this._roomInfo.PlayerCount} / {this._roomInfo.MaxPlayers})";

            buttom.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(this._roomInfo.Name));
        }
    }
    
    void OnEnterRoom(string roomName)
    {
        if(cshPhotonManager.instance.readyToJoin)
        {
            PhotonNetwork.JoinRoom(roomName);
        }
    }
}
