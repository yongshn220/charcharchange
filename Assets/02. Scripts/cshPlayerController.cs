using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshPlayerController : MonoBehaviour
{

    private GameObject player;
   
  
    private Vector3 m_velocity; // 3���� ����. ĳ���Ͱ� �̵��� ����
   

    public cshJoystick sJoystick; // background�� ������ �ִ� ��ũ��Ʈ(���� �е带 x,y������ ��ŭ �̵���Ű�� �ִ��� �������� ����) 
    public float m_moveSpeed = 2.0f; // ĳ���� �̵� �ӵ�
                                   

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
        PlayerMove(); // �̵��ϴ� �Լ� ȣ��
       
    }


    private void PlayerMove()
    {
        
      
        float h = sJoystick.GetHorizontalValue(); // joystick���� x������ �󸶸�ŭ ���������� 
        float v = sJoystick.GetVerticalValue(); // joystick���� y������ �󸶸�ŭ ����������
        m_velocity = new Vector3(h, 0, v); // x, z �� 
        m_velocity = m_velocity.normalized;

       

        transform.Translate(m_velocity * m_moveSpeed * Time.deltaTime, Space.World);

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
                if (player0.gameObject.activeInHierarchy) // ù��° ĳ���Ͷ�� 
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