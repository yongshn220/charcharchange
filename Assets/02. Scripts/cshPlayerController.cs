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
    public float m_moveSpeed = 6.0f; // ĳ���� �̵� �ӵ�
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

    void Start()
    {
        photonView = transform.GetComponent<PhotonView>();
        
        if(!photonView.IsMine)
        {
            return;
        }

        idleSpeed = m_moveSpeed;
        anim = transform.GetComponent<Animation>();

        ResultText.enabled = false;
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

        if (score == 3)
        {
            Debug.Log("win!");
            ResultText.enabled = true;
        }
        if (hp == 0)
        {
            Debug.Log("die");
            ResultText.enabled = true;
            ResultText.text = "GAME OVER";
        }

         textScore.text = "Score: " + score.ToString() + "HP: " + hp.ToString();
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


    public void OnPointerDown()
    {
        m_moveSpeed = m_moveSpeed * 1.2f;
    }

    public void OnPointerUp()
    {
        m_moveSpeed = idleSpeed;
    }

    [PunRPC]
    public void changePlayer(int playerId)
    {
        GameObject npc = cshGameManager.instance.spawnPrefabs[playerId];

        Mesh mesh = npc.GetComponent<MeshFilter>().sharedMesh;

        MeshFilter mf = transform.GetComponent<MeshFilter>();
       
        mf.sharedMesh = mesh;

        rotatePlayer();
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
            
            npcControl npc = collision.gameObject.GetComponent<npcControl>();
            int id = collision.gameObject.GetComponent<PhotonView>().ViewID;

            photonView.RPC("DestroyNpc", RpcTarget.All, id);

            if (playerId == npc.id)
            {
                score++;

                idList.Remove(playerId);

                int newPlayerIdIndex = Random.Range(0, idList.Count); // 0, 1
                int newPlayerId = idList[newPlayerIdIndex];

                playerId = newPlayerId;

                photonView.RPC("changePlayer", RpcTarget.All, playerId);
            }
            else
            {
                hp--;
            }
        }
    }

    [PunRPC]
    private void DestroyNpc(int id)
    {
        Destroy(PhotonView.Find(id).gameObject);
    }
}