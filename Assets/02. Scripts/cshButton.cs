using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cshButton : MonoBehaviour
{
    public Button btnAccel;
    public cshPlayerController sPlayer;

    void Start()
    {
        btnAccel.gameObject.SetActive(true); // btnAccel ��ư Ȱ��ȭ 
        btnAccel.onClick.RemoveAllListeners(); // �ϴ� ��ư�� Ŭ���� ���õ� ��� �̺�Ʈ ���� 
        btnAccel.onClick.AddListener(OnClickAccelButton); // �Լ��� �̺�Ʈ �����ʷ� �߰� 

    }
    void Update()
    {
          /*UpdateButton();*/
    }

 /*   private void UpdateButton()
    {
        //bool canAttack = sPlayer.CanAttack(); // player ��ũ��Ʈ�� canAttack �Լ� ȣ��

        //btnAttack.gameObject.SetActive(canAttack);  // ���� ������ ����� ������  attack ��ư Ȱ��ȭ 
        btnAccel.gameObject.SetActive(true); // ������ jump ��ư Ȱ��ȭ 
    }
*/

    private void OnClickAccelButton()
    {
        sPlayer.OnVirtualPadAccel();
        Debug.Log("button click");
    }
}

