using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;
using UnityEngine.SceneManagement;

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

    public int m_moveSpeed;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_moveSpeed = 15;

        playerId = Random.Range(0, spawnPrefabs.Length);

        playerId = 0;

        Debug.Log("gameManager playerId :" +playerId);

        this.player = PhotonNetwork.Instantiate(this.playerPrefeab.name, new Vector3(24,0,-21), Quaternion.identity, 0);

        setPlayerSetting();
    }


    /*
        [ContextMenu("Down")]*/
    public void OnPointerDown()
    {
        /*  m_moveSpeed = m_moveSpeed * 1.2f;*/
        m_moveSpeed = 18;
        Debug.Log("onPointerDown " + m_moveSpeed);
    }

    /*   [ContextMenu("Up")]*/
    public void OnPointerUp()
    {
        /* m_moveSpeed = idleSpeed;*/
        m_moveSpeed = 15;
        Debug.Log("OnPointerUp " + m_moveSpeed);

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

    public void Lobby()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lobby");
    }
}
