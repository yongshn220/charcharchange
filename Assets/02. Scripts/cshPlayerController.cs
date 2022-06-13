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




    void Start()
    {
        //cshGameManager.instance.spawnPrefabs[0];


        playerRigidbody = gameObject.GetComponent<Rigidbody>();

        cshGameManager = GameObject.Find("GameManager").GetComponent<cshGameManager>();

        playerId = cshGameManager.playerId;
        idList.Remove(playerId);



        Debug.Log("gameManager�κ��� ������ id" + playerId);


        for (int i = 0; i < idList.Count; i++)
        {
            Debug.Log(idList[i]);
        }


        //GameObject npc = Resources.Load<GameObject>("npc"+playerId.ToString());

        //npc.GetComponent

        changePlayer(playerId);



        textScore = GameObject.Find("Score").GetComponent<Text>();




    }

    void Update()
    {
        PlayerMove(); 

   /*     if(score == 3)
        {
            Debug.Log("win!");
        }
        if(hp == 0)
        {
            Debug.Log("die");
        }*/

       // textScore.text = "Score: " + score.ToString() + "HP: " + hp.ToString();



     


    }


    private void PlayerMove()
    {

        float h = sJoystick.GetHorizontalValue(); // joystick���� x������ �󸶸�ŭ ���������� 
        float v = sJoystick.GetVerticalValue(); // joystick���� y������ �󸶸�ŭ ����������
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
        Debug.Log("curRot:" + curRot);

        angle = angle * Mathf.Rad2Deg;
        Debug.Log("h:"+ angle);

        angle = angle + curRot;
        Quaternion targetAngle = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        Debug.Log(targetAngle);
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Trigger IN");
        if (collision.gameObject.tag == "npc")
        {
            Debug.Log("Trigger In2");
            cshNpcController npc = collision.gameObject.GetComponent<cshNpcController>();
            Debug.Log(npc.id + "�� �浹");

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