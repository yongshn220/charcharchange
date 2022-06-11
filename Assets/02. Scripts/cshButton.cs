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
        btnAccel.gameObject.SetActive(true); // btnAccel 버튼 활성화 
        btnAccel.onClick.RemoveAllListeners(); // 일단 버튼의 클릭과 관련된 모든 이벤트 제거 
        btnAccel.onClick.AddListener(OnClickAccelButton); // 함수를 이벤트 리스너로 추가 

    }
    void Update()
    {
          /*UpdateButton();*/
    }

 /*   private void UpdateButton()
    {
        //bool canAttack = sPlayer.CanAttack(); // player 스크립트의 canAttack 함수 호출

        //btnAttack.gameObject.SetActive(canAttack);  // 공격 가능한 대상이 있으면  attack 버튼 활성화 
        btnAccel.gameObject.SetActive(true); // 없으면 jump 버튼 활성화 
    }
*/

    private void OnClickAccelButton()
    {
        sPlayer.OnVirtualPadAccel();
        Debug.Log("button click");
    }
}

