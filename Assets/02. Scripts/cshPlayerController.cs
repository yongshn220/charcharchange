using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshPlayerController : MonoBehaviour
{

    private GameObject player;
   
    //private Animator m_animator; // animation 속성을 담고있는 animator 컴포넌트를 불러올 변수
    private Vector3 m_velocity; // 3차원 벡터. 캐릭터가 이동될 방향
    //private bool m_isGrounded = true; // 현재 캐릭터가 땅 위에 있는지 공중에 있는지 판단하는 변수
    //private bool m_jumpOn = false; // 점프 버튼이 눌리면 true로

    public cshJoystick sJoystick; // background가 가지고 있는 스크립트(가상 패드를 x,y축으로 얼만큼 이동시키고 있는지 가져오기 위함) 
    public float m_moveSpeed = 2.0f; // 캐릭터 이동 속도
   // public float m_jumpForce = 5.0f; // 점프 높이 



    void Start()
    {
        //m_animator = GetComponent<Animator>(); // 현재 캐릭터가 가지고 있는 애니메이터 속성 가져옴 
       // m_attackArea = GetComponentInChildren<cshAttackArea>(); // 자식이 가지고 있는 attackArea라는 속성을 가져옴 

        player = GetComponent<GameObject>();

    }

    void Update()
    {
        PlayerMove(); // 이동하는 함수 호출
       
    }


    private void PlayerMove()
    {
        CharacterController controller = GetComponent<CharacterController>();
        float gravity = 20.0f; // regidbody 안주기 때문에 중력 직접 구현 

     
        float h = sJoystick.GetHorizontalValue(); // joystick에서 x축으로 얼마만큼 움직였는지 
        float v = sJoystick.GetVerticalValue(); // joystick에서 y축으로 얼마만큼 움직였는지
        m_velocity = new Vector3(h, 0, v); // x, z 축 
        m_velocity = m_velocity.normalized;

        //player.SetFloat("Move", m_velocity.magnitude); // magnitude: 길이. (1,0,1)이면 루트 2

        transform.Translate(m_velocity * m_moveSpeed * Time.deltaTime, Space.World);

        transform.LookAt(transform.position + m_velocity); // 현재 캐릭터가 이동하려고 하는 방향으로 쳐다봄

       

        controller.Move(m_velocity * m_moveSpeed * Time.deltaTime); // charactor controller에 있는 move함수
        // 현재 지정한 방향, 크기, 플랫폼에 상관없이 캐릭터가 move

    }

  


    public void OnVirtualPadAccel() // accel 버튼 누르면 실행 
    {
        m_moveSpeed = 10.0f;
    }

}