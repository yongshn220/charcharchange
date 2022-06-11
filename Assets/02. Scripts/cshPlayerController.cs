using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshPlayerController : MonoBehaviour
{

    private GameObject player;
   
  
    private Vector3 m_velocity; // 3차원 벡터. 캐릭터가 이동될 방향
   

    public cshJoystick sJoystick; // background가 가지고 있는 스크립트(가상 패드를 x,y축으로 얼만큼 이동시키고 있는지 가져오기 위함) 
    public float m_moveSpeed = 2.0f; // 캐릭터 이동 속도
   // public float m_jumpForce = 5.0f; // 점프 높이 



    void Start()
    {
     
        player = GetComponent<GameObject>();

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

  


/*    public void OnVirtualPadAccel() // accel 버튼 누르면 실행 
    {
        m_moveSpeed = 10.0f;
    }
*/
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

}