using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class cshPlayerController : MonoBehaviour
{

 


    private Vector3 m_velocity; // 3���� ����. ĳ���Ͱ� �̵��� ����

    private Vector3 m_rotation;

    public cshJoystick sJoystick; // background�� ������ �ִ� ��ũ��Ʈ(���� �е带 x,y������ ��ŭ �̵���Ű�� �ִ��� �������� ����) 
    public float m_moveSpeed = 6.0f; // ĳ���� �̵� �ӵ�
    public GameObject player;

    public int hp = 3;

    private Rigidbody playerRigidbody;
    public cshGameManager cshGameManager;
    public int playerId;
    public GameObject cloneNpc;

    /*public List<int> idList = new List<int>();*/

    public int score = 0;
    private Text textScore;
    List<int> idList = Enumerable.Range(0, 3).ToList();

    public GameObject parentPlayer;

    public CinemachineVirtualCamera cvCam;

    public Text ResultText;

    private Animation anim;

    void Start()
    {

        anim = transform.GetComponent<Animation>();

        ResultText.enabled = false;
        playerRigidbody = gameObject.GetComponent<Rigidbody>();

        cshGameManager = GameObject.Find("GameManager").GetComponent<cshGameManager>();

        playerId = cshGameManager.playerId;
        idList.Remove(playerId);

        changePlayer(playerId);

        textScore = GameObject.Find("Score").GetComponent<Text>();

        rotatePlayer();


    }

    [ContextMenu("rotate")]
    void rotatePlayer()
    {
        cvCam.Follow = null;
        anim.Play("rotate");

        

        Invoke("followPlayer", 1);
    }

    void followPlayer()
    {
        cvCam.Follow = player.transform;
    }

    void Update()
    {
        PlayerMove();

        if (score == 3)
        {
            Debug.Log("win!");
            ResultText.enabled = true;
        }
        if (hp == 0)
        {
            Debug.Log("die");
        }
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

        Debug.Log(angle);
        float curRot = transform.eulerAngles.y;

        angle = angle * Mathf.Rad2Deg;

        angle = angle + curRot;
        Quaternion targetAngle = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, 2.0f * Time.deltaTime);
    }


    public void OnPointerDown()
    {
        m_moveSpeed = 10.0f;
        Debug.Log("��ư ������ ��");
    }

    public void OnPointerUp()
    {
        m_moveSpeed = 2.0f;
        Debug.Log("��ư �� ");
    }

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
        Debug.Log("Trigger IN");
        if (collision.gameObject.tag == "npc")
        {
            
            cshNpcController npc = collision.gameObject.GetComponent<cshNpcController>();
          

            Destroy(collision.gameObject);

            Debug.Log("playerId: " + playerId + "npcId: " + npc.id);

            if (playerId == npc.id)
            {
                score++;

                idList.Remove(playerId);

                Debug.Log("score: "+score);
            

                int newPlayerIdIndex = Random.Range(0, idList.Count); // 0, 1

                int newPlayerId = idList[newPlayerIdIndex];

                Debug.Log("newPlayerID: "+newPlayerId);


                changePlayer(newPlayerId);

               // rotatePlayer();

                playerId = newPlayerId;


            }
            else
            {
                hp--;
                Debug.Log("hp: "+ hp);
            }




        }
    }



}