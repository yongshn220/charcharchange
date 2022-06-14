using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class cshPhotonManager : MonoBehaviourPunCallbacks
{
    public static cshPhotonManager instance;
    private string userId = "Anon";

    public bool readyToJoin;
    public TMP_InputField userIdText;
    public TMP_InputField roomNameText;

    public GameObject roomPref;
    public GameObject roomContents;

    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();
    void Awake()
    {
        instance = this;

        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.NickName = userId;

        PhotonNetwork.ConnectUsingSettings();
    }

    void Start()
    {
        userId = PlayerPrefs.GetString("userId", $"USER_{Random.Range(0, 100):00}");
        
        userIdText.text = userId;

        PhotonNetwork.NickName = userId;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("1. connected server");
        PhotonNetwork.JoinLobby();
    }

    // [ContextMenu("OnJoinedLobby")]
    public override void OnJoinedLobby()
    {
        Debug.Log("2. connected Lobby");
        
        readyToJoin = true;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("3. join random room fail");

        PhotonNetwork.CreateRoom("NewRoom");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("room created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("5. Room joined.");

        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameScene");
            // Debug.Log("Room Entered");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom;

        Debug.Log("aaa");
        foreach(var room in roomList)
        {
            Debug.Log("aa");
            if(room.RemovedFromList)
            {
                roomDict.TryGetValue(room.Name, out tempRoom);

                Destroy(tempRoom);

                roomDict.Remove(room.Name);
            }
            else
            {
                if(!roomDict.ContainsKey(room.Name))
                {
                    Debug.Log("a");
                    GameObject _room = Instantiate(roomPref, roomContents.transform);

                    _room.GetComponent<cshRoomData>().RoomInfo = room;
                    roomDict.Add(room.Name, _room);
                }
                else
                {
                    roomDict.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<cshRoomData>().RoomInfo = room;
                }

            }
        }
    }

    // When room create button clicked
    public void OnCreateRoomBtnClk()
    {
        if(!readyToJoin)
        {
            return;
        }
        PlayerPrefs.SetString("userId", this.userIdText.text);
        PhotonNetwork.NickName = this.userIdText.text;

        RoomOptions op = new RoomOptions();
        op.IsOpen = true;
        op.IsVisible = true;
        op.MaxPlayers = 2;

        if(string.IsNullOrEmpty(roomNameText.text))
        {
            roomNameText.text = $"ROOM_{Random.Range(0,100):00}";
        }

        PhotonNetwork.CreateRoom(roomNameText.text, op);
    }
}
