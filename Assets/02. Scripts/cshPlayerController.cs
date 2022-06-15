using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class cshPlayerController : MonoBehaviour
{
    private PhotonView photonView;
    private Vector3 m_velocity; // 3���� ����. ĳ���Ͱ� �̵��� ����

    public cshJoystick sJoystick; // background�� ������ �ִ� ��ũ��Ʈ(���� �е带 x,y������ ��ŭ �̵���Ű�� �ִ��� �������� ����) 
   // private float m_moveSpeed = 15.0f; // ĳ���� �̵� �ӵ�

    public int m_moveSpeed;

    private float idleSpeed;
    public int hp = 3;

    private Rigidbody playerRigidbody;
    public cshGameManager cshGameManager;
    public int playerId;
    public int score = 0;
    private Text textScore;

    List<int> idList = Enumerable.Range(0, 3).ToList();

    public CinemachineVirtualCamera cvCam;

    public Text ResultText;

    private Animation anim;

    private AudioSource audio;

    private bool isGameOver = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        photonView = transform.GetComponent<PhotonView>();
        
        ResultText = cshGameManager.instance.resultText;

      

        if (!photonView.IsMine)
        {
            return;
        }

        /*idleSpeed = m_moveSpeed;*/
        /*m_moveSpeed = 15;*/
        anim = transform.GetComponent<Animation>();

        ResultText.gameObject.SetActive(false);
        playerRigidbody = gameObject.GetComponent<Rigidbody>();

        cshGameManager = cshGameManager.instance;

        playerId = cshGameManager.playerId;
        idList.Remove(playerId);

        changePlayer(playerId);

        textScore = GameObject.Find("Score").GetComponent<Text>();

        rotatePlayer();
    }

    [ContextMenu("rotate")]
    void rotatePlayer()
    {
        if(!photonView.IsMine)
        {
            return;
        }
        cvCam.Follow = null;
        anim.Play("rotate");

        Invoke("followPlayer", 1);
    }

    void followPlayer()
    {
        cvCam.Follow = transform;
    }

    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        PlayerMove();

        m_moveSpeed = cshGameManager.instance.m_moveSpeed;


        Debug.Log("moveSpeed: " + cshGameManager.instance.m_moveSpeed);
         textScore.text = "Score: " + score.ToString() + "   HP: " + hp.ToString();
    }

    private void PlayerMove()
    {
        float h = sJoystick.GetHorizontalValue(); // joystick���� x������ �󸶸�ŭ ���������� 
        float v = sJoystick.GetVerticalValue(); // joystick���� y������ �󸶸�ŭ ����������

        if(h == 0 || v == 0)
        {
            return;
        }
        m_velocity = new Vector3(0, 0, v); // x, z �� 
        m_velocity = m_velocity.normalized;

        if(v > 0)
        {
            transform.position += transform.forward * m_moveSpeed * Time.deltaTime;
        }
        else if (v < 0)
        {
            transform.position += -transform.forward * m_moveSpeed * Time.deltaTime;
        }

        float angle = h;

        float curRot = transform.eulerAngles.y;

        angle = angle * Mathf.Rad2Deg;

        angle = angle + curRot;
        Quaternion targetAngle = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, 2.0f * Time.deltaTime);
    }


    [PunRPC]
    public void changePlayer(int playerId)
    {
        GameObject npc = cshGameManager.instance.spawnPrefabs[playerId];

        Mesh mesh = npc.GetComponent<MeshFilter>().sharedMesh;

        MeshFilter mf = transform.GetComponent<MeshFilter>();
       
        mf.sharedMesh = mesh;

        rotatePlayer();
        audio.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!photonView.IsMine)
        {
            return;
        }
        Debug.Log("Trigger IN");
        if (collision.gameObject.tag == "npc")
        {
            if(isGameOver) { return; }
            npcControl npc = collision.gameObject.GetComponent<npcControl>();
            int id = collision.gameObject.GetComponent<PhotonView>().ViewID;

            photonView.RPC("DestroyNpc", RpcTarget.All, id);

            if (playerId == npc.id)
            {
                score++;
                ScoreCheck();

                if(isGameOver)
                {
                    rotatePlayer();
                    audio.Play();
                    return;
                }
                idList.Remove(playerId);

                int newPlayerIdIndex = Random.Range(0, idList.Count); // 0, 1
                int newPlayerId = idList[newPlayerIdIndex];

                playerId = newPlayerId;

                photonView.RPC("changePlayer", RpcTarget.All, playerId);
            }
            else
            {
                hp--;
                ScoreCheck();
            }
        }
    }

    [PunRPC]
    private void DestroyNpc(int id)
    {
        Destroy(PhotonView.Find(id).gameObject);
    }

    private void ScoreCheck()
    {
        if (score == 3)
        {
            Win();
            photonView.RPC("Lose", RpcTarget.Others);
        }
        if (hp == 0)
        {
            Lose();
            photonView.RPC("Win", RpcTarget.Others);
        }
    }

    [PunRPC]
    private void Win()
    {
        ResultText.text = "WIN!!";
        ResultText.gameObject.SetActive(true);
        isGameOver = true;
    }

    [PunRPC]
    private void Lose()
    {
        ResultText.text = "GAME OVER";
        ResultText.gameObject.SetActive(true);
        isGameOver = true;
    }
}