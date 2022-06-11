using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshPlayerController : MonoBehaviour
{

    private GameObject player;
   
  
    private Vector3 m_velocity; // 3���� ����. ĳ���Ͱ� �̵��� ����
   

    public cshJoystick sJoystick; // background�� ������ �ִ� ��ũ��Ʈ(���� �е带 x,y������ ��ŭ �̵���Ű�� �ִ��� �������� ����) 
    public float m_moveSpeed = 2.0f; // ĳ���� �̵� �ӵ�
   // public float m_jumpForce = 5.0f; // ���� ���� 



    void Start()
    {
     
        player = GetComponent<GameObject>();

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

  


/*    public void OnVirtualPadAccel() // accel ��ư ������ ���� 
    {
        m_moveSpeed = 10.0f;
    }
*/
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

}