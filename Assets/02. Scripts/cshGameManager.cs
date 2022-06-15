using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class cshGameManager : MonoBehaviourPun
{
    public GameObject[] spawnPrefabs;
    public int playerId;
    public Mesh[] meshes;

    public static cshGameManager instance;

    public GameObject playerPrefeab;


    public cshJoystick JoyStickcs;
    public Text resultText;
    public CinemachineVirtualCamera cvCamera;

    private GameObject player;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerId = Random.Range(0, spawnPrefabs.Length);

        playerId = 0;

        Debug.Log("gameManager playerId :" +playerId);

        this.player = PhotonNetwork.Instantiate(this.playerPrefeab.name, new Vector3(24,0,-21), Quaternion.identity, 0);

        setPlayerSetting();
    }

    void setPlayerSetting()
    {
        cshPlayerController pc = this.player.GetComponent<cshPlayerController>();

        pc.sJoystick = JoyStickcs;
        pc.ResultText = resultText;
        pc.cvCam = cvCamera;

        cvCamera.Follow = pc.transform;
        cvCamera.LookAt = pc.transform;
    }
}
