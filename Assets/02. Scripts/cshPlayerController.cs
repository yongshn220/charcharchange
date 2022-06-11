using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshPlayerController : MonoBehaviour
{

    private GameObject player;
   
    //private Animator m_animator; // animation �Ӽ��� ����ִ� animator ������Ʈ�� �ҷ��� ����
    private Vector3 m_velocity; // 3���� ����. ĳ���Ͱ� �̵��� ����
    //private bool m_isGrounded = true; // ���� ĳ���Ͱ� �� ���� �ִ��� ���߿� �ִ��� �Ǵ��ϴ� ����
    //private bool m_jumpOn = false; // ���� ��ư�� ������ true��

    public cshJoystick sJoystick; // background�� ������ �ִ� ��ũ��Ʈ(���� �е带 x,y������ ��ŭ �̵���Ű�� �ִ��� �������� ����) 
    public float m_moveSpeed = 2.0f; // ĳ���� �̵� �ӵ�
   // public float m_jumpForce = 5.0f; // ���� ���� 



    void Start()
    {
        //m_animator = GetComponent<Animator>(); // ���� ĳ���Ͱ� ������ �ִ� �ִϸ����� �Ӽ� ������ 
       // m_attackArea = GetComponentInChildren<cshAttackArea>(); // �ڽ��� ������ �ִ� attackArea��� �Ӽ��� ������ 

        player = GetComponent<GameObject>();

    }

    void Update()
    {
        PlayerMove(); // �̵��ϴ� �Լ� ȣ��
       
    }


    private void PlayerMove()
    {
        CharacterController controller = GetComponent<CharacterController>();
        float gravity = 20.0f; // regidbody ���ֱ� ������ �߷� ���� ���� 

     
        float h = sJoystick.GetHorizontalValue(); // joystick���� x������ �󸶸�ŭ ���������� 
        float v = sJoystick.GetVerticalValue(); // joystick���� y������ �󸶸�ŭ ����������
        m_velocity = new Vector3(h, 0, v); // x, z �� 
        m_velocity = m_velocity.normalized;

        //player.SetFloat("Move", m_velocity.magnitude); // magnitude: ����. (1,0,1)�̸� ��Ʈ 2

        transform.Translate(m_velocity * m_moveSpeed * Time.deltaTime, Space.World);

        // m_velocity.y -= gravity * Time.deltaTime; // velocity.y�� gravity��ŭ ���� 
        // ex) �������̸� ���߿� ��. �ٽ� -20��ŭ �����ӵ��� ������ ���� ������ �� �ְ� 
        // rigidbody ���� ������ �ʿ� 

        controller.Move(m_velocity * m_moveSpeed * Time.deltaTime); // charactor controller�� �ִ� move�Լ�
        // ���� ������ ����, ũ��, �÷����� ������� ĳ���Ͱ� move

        //m_isGrounded = controller.isGrounded; // ĳ���Ͱ� �� ���� ������ true, ���߿� ������ false�� 
    }

}