using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class cshPlayerController : MonoBehaviour
{



    private Vector3 m_velocity; // 3차원 벡터. 캐릭터가 이동될 방향


    public cshJoystick sJoystick; // background가 가지고 있는 스크립트(가상 패드를 x,y축으로 얼만큼 이동시키고 있는지 가져오기 위함) 
    public float m_moveSpeed = 4.0f; // 캐릭터 이동 속도


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

        Debug.Log("gameManager로부터 가져온 id" + playerId);

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

        float h = sJoystick.GetHorizontalValue(); // joystick에서 x축으로 얼마만큼 움직였는지 
        float v = sJoystick.GetVerticalValue(); // joystick에서 y축으로 얼마만큼 움직였는지
        m_velocity = new Vector3(h, 0, v); // x, z 축 
        m_velocity = m_velocity.normalized;


        playerRigidbody.position += m_velocity * m_moveSpeed * Time.deltaTime;



        // transform.Translate(m_velocity * m_moveSpeed * Time.deltaTime, Space.World);

        transform.LookAt(transform.position + m_velocity); // 현재 캐릭터가 이동하려고 하는 방향으로 쳐다봄
    }


    public void OnPointerDown()
    {
        m_moveSpeed = 10.0f;
        Debug.Log("버튼 눌리는 중");
    }

    public void OnPointerUp()
    {
        m_moveSpeed = 2.0f;
        Debug.Log("버튼 땜 ");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Trigger IN");
        if (collision.gameObject.tag == "npc")
        {
            Debug.Log("Trigger In2");
            cshNpcController npc = collision.gameObject.GetComponent<cshNpcController>();
            Debug.Log( npc.id + "와 충돌");

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

                int newPlayerIdIndex = Random.Range(0, idList.Count); // 0, 1, 2 중 하나

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