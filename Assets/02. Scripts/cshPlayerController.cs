using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class cshPlayerController : MonoBehaviour
{



    private Vector3 m_velocity; // 3���� ����. ĳ���Ͱ� �̵��� ����


    public cshJoystick sJoystick; // background�� ������ �ִ� ��ũ��Ʈ(���� �е带 x,y������ ��ŭ �̵���Ű�� �ִ��� �������� ����) 
    public float m_moveSpeed = 4.0f; // ĳ���� �̵� �ӵ�


    public int hp = 3;

    private Rigidbody playerRigidbody;

    public cshGameManager cshGameManager;

    public int playerId;

    public GameObject cloneNpc;

    /*public List<int> idList = new List<int>();*/

    public int score = 0;

    private Text textScore;

    List<int> idList = Enumerable.Range(1, 4).ToList();

    public GameObject parentPlayer;




    void Start()
    {
     

        playerRigidbody = gameObject.GetComponent<Rigidbody>();

        cshGameManager = GameObject.Find("GameManager").GetComponent<cshGameManager>();

        playerId = cshGameManager.playerId;

        //idList.Add(playerId);

        idList.Remove(playerId);

        Debug.Log("gameManager�κ��� ������ id" + playerId);

        GameObject npc = Resources.Load<GameObject>("npc"+playerId.ToString());


        cloneNpc = Instantiate(npc, transform);
        cloneNpc.transform.position = parentPlayer.transform.position;

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

        //textScore.text = "Score: " + score.ToString() + "HP: " + hp.ToString();



     


    }


    private void PlayerMove()
    {

        float h = sJoystick.GetHorizontalValue(); // joystick���� x������ �󸶸�ŭ ���������� 
        float v = sJoystick.GetVerticalValue(); // joystick���� y������ �󸶸�ŭ ����������
        m_velocity = new Vector3(h, 0, v); // x, z �� 
        m_velocity = m_velocity.normalized;


        playerRigidbody.position += m_velocity * m_moveSpeed * Time.deltaTime;



        // transform.Translate(m_velocity * m_moveSpeed * Time.deltaTime, Space.World);

        transform.LookAt(transform.position + m_velocity); // ���� ĳ���Ͱ� �̵��Ϸ��� �ϴ� �������� �Ĵٺ�
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Trigger IN");
        if (collision.gameObject.tag == "npc")
        {
            Debug.Log("Trigger In2");
            cshNpcController npc = collision.gameObject.GetComponent<cshNpcController>();
            Debug.Log( npc.id + "�� �浹");

            Destroy(collision.gameObject);

            if (playerId == npc.id)
            {
                score++;
                Debug.Log("score: "+score);
                // int newPlayerId = Random.Range(0, 3) + 1;

                /*
                     while (idList.Contains(newPlayerId))
                     {
                         newPlayerId = Random.Range(0, 3) + 1;


                     }*/

                // {0,1,4}

                int newPlayerIdIndex = Random.Range(0, idList.Count); // 0, 1, 2 �� �ϳ�

                int newPlayerId = idList[newPlayerIdIndex];

                Debug.Log("newPlayerID: "+newPlayerId);

                //idList.Add(newPlayerId);

                Destroy(cloneNpc);

                
                GameObject newNpc = Resources.Load<GameObject>("npc" + newPlayerId.ToString());
                cloneNpc = Instantiate(newNpc, transform);
                cloneNpc.transform.position = parentPlayer.transform.position;




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