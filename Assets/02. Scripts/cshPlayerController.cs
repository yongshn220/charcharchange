using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshPlayerController : MonoBehaviour
{

    private GameObject player;
   
  
    private Vector3 m_velocity; // 3차원 벡터. 캐릭터가 이동될 방향
   

    public cshJoystick sJoystick; // background가 가지고 있는 스크립트(가상 패드를 x,y축으로 얼만큼 이동시키고 있는지 가져오기 위함) 
    public float m_moveSpeed = 2.0f; // 캐릭터 이동 속도
                                   

    public GameObject player0, player1, player2;

    public int hp = 3;




    void Start()
    {
     
        player = GetComponent<GameObject>();

     

       /* player1 = transform.GetChild(0).gameObject;
        player2 = transform.GetChild(1).gameObject;
        player3 = transform.GetChild(2).gameObject;

*/
        player0.gameObject.SetActive(true);
        player1.gameObject.SetActive(false);
        player2.gameObject.SetActive(false);

    }

    void Update()
    {
        PlayerMove(); // 이동하는 함수 호출
       
    }


    private void PlayerMove()
    {
        
      
        float h = sJoystick.GetHorizontalValue(); // joystick에서 x축으로 얼마만큼 움직였는지 
        float v = sJoystick.GetVerticalValue(); // joystick에서 y축으로 얼마만큼 움직였는지
        m_velocity = new Vector3(h, 0, v); // x, z 축 
        m_velocity = m_velocity.normalized;

       

        transform.Translate(m_velocity * m_moveSpeed * Time.deltaTime, Space.World);

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

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "npc")
        {
           // cshNpcController npc = other.gameObject.GetComponent<cshNpcController>();
           // npc.id

            Destroy(other.gameObject);

   

            /* {
                 hp--;
                 if (hp == 0) Debug.Log("Game Over");

             }*/
            if (other.gameObject.name == "npc1")
            {
                if (player0.gameObject.activeInHierarchy) // 첫번째 캐릭터라면 
                {

                    player1.SetActive(true);
                    player0.SetActive(false);
                }
            }
            else if (other.gameObject.name == "npc2")
            {
                if (player1.gameObject.activeInHierarchy)
                {
                    player1.SetActive(false);
                    player2.SetActive(true);
                }
            }
            else if (other.gameObject.name == "npc3")
            {
                if (player2.gameObject.activeInHierarchy)
                {
                    Debug.Log("Win!!");
                }
            }
            else
            {
                hp--;
                if (hp == 0) Debug.Log("Game Over");
            }



        }
    }



}