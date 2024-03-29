using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class cshJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image imgBG;
    private Image imgJoystick;
    private Vector3 vInputVector;


    // Start is called before the first frame update
    void Start()
    {
        imgBG = GetComponent<Image>(); // background 이미지 저장
        imgJoystick = transform.GetChild(0).GetComponent<Image>(); // 0번째 자식(stick)이 가진 이미지 저장 
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*Debug.Log("Joystick >>> OnDrag()");*/
        Vector2 pos;

        //배경 영역에 터치가 발생할 때
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgBG.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {   // background를 원점으로 하는 좌표로 바꿔줌 
            //Debug.Log(imgBG.rectTransform.sizeDelta);
            //터치된 로컬 좌표값을 pos에 저장
            //배경 이미지의 size로 나누어 pos.x: -1~1, pos.y: -1~1 으로 변환
            pos.x = (pos.x / imgBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / imgBG.rectTransform.sizeDelta.y);

            vInputVector = new Vector3(pos.x, pos.y, 0);


            vInputVector = (vInputVector.magnitude > 1.0f) ? vInputVector.normalized : vInputVector;
            // 벡터의 길이가 1보다 크면 1로

            //Joystick Image 움직임
            imgJoystick.rectTransform.anchoredPosition = new Vector3(vInputVector.x * (imgBG.rectTransform.sizeDelta.x / 2),
                                                                    vInputVector.y * (imgBG.rectTransform.sizeDelta.y / 2));

           /* Debug.Log("vInputVector: " + vInputVector);*/
        }
    }

    public void OnPointerDown(PointerEventData eventData) // eventData: 마우스 좌표 정보 포함 
    { // 이미지 안에 마우스 포인터가 클릭 되었다면
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData) // 떼면 0으로 
    {
        vInputVector = Vector3.zero;
        //vInputVector = new Vector3(pos.x, pos.y, 0);

        imgJoystick.rectTransform.anchoredPosition = Vector3.zero;
    }

    //PlayerController 에서 입력 값을 넘겨주기 위한 함수
    public float GetHorizontalValue()
    {
        return vInputVector.x;
    }
    public float GetVerticalValue()
    {
        return vInputVector.y;
    }





}