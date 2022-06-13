using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class cshPlayerController : MonoBehaviour
{

 


    private Vector3 m_velocity; // 3차원 벡터. 캐릭터가 이동될 방향

    private Vector3 m_rotation;

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

    List<int> idList = Enumerable.Range(0, 3).ToList();

    public GameObject parentPlayer;




    void Start()
    {
        //cshGameManager.instance.spawnPrefabs[0];


        playerRigidbody = gameObject.GetComponent<Rigidbody>();

        cshGameManager = GameObject.Find("GameManager").GetComponent<cshGameManager>();

        playerId = cshGameManager.playerId;
        idList.Remove(playerId);



        Debug.Log("gameManager로부터 가져온 id" + playerId);


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

     /*   float h = sJoystick.GetHorizontalValue(); // joystick에서 x축으로 얼마만큼 움직였는지 
        float v = sJoystick.GetVerticalValue(); // joystick에서 y축으로 얼마만큼 움직였는지
        m_velocity = new Vector3(h, 0, v); // x, z 축 
        m_velocity = m_velocity.normalized;


        playerRigidbody.position += m_velocity * m_moveSpeed * Time.deltaTime;

*/

        float h = sJoystick.GetHorizontalValue(); // joystick에서 x축으로 얼마만큼 움직였는지 
        float v = sJoystick.GetVerticalValue(); // joystick에서 y축으로 얼마만큼 움직였는지
        m_velocity = new Vector3(0, 0, v); // x, z 축 
        m_velocity = m_velocity.normalized;

        //Debug.Log("h :" + h + "v :" + v);




        playerRigidbody.position += m_velocity * m_moveSpeed * Time.deltaTime;

        //TargetRotation = Quaternion.Euler(0, v*90, 0);

        //h = Mathf.Rad2Deg * h;
        float angle = h;

        float curRot = transform.eulerAngles.y;
        Debug.Log("curRot:" + curRot);

        angle = angle * Mathf.Rad2Deg;
        Debug.Log("h:"+ angle);

        angle = angle + curRot;
        Quaternion targetAngle = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        Debug.Log(targetAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, 0.1f * Time.deltaTime);


        //transform.Rotate(0, v, 0);

        //x.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(1, 360), 0));

        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, v)).normalized;
        // v만큼을 rotation으로 


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
            Debug.Log(npc.id + "와 충돌");

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