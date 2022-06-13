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
        imgBG = GetComponent<Image>(); // background �̹��� ����
        imgJoystick = transform.GetChild(0).GetComponent<Image>(); // 0��° �ڽ�(stick)�� ���� �̹��� ���� 
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*Debug.Log("Joystick >>> OnDrag()");*/
        Vector2 pos;

        //��� ������ ��ġ�� �߻��� ��
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(imgBG.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {   // background�� �������� �ϴ� ��ǥ�� �ٲ��� 
            //Debug.Log(imgBG.rectTransform.sizeDelta);
            //��ġ�� ���� ��ǥ���� pos�� ����
            //��� �̹����� size�� ������ pos.x: -1~1, pos.y: -1~1 ���� ��ȯ
            pos.x = (pos.x / imgBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / imgBG.rectTransform.sizeDelta.y);

            vInputVector = new Vector3(pos.x, pos.y, 0);


            vInputVector = (vInputVector.magnitude > 1.0f) ? vInputVector.normalized : vInputVector;
            // ������ ���̰� 1���� ũ�� 1��

            //Joystick Image ������
            imgJoystick.rectTransform.anchoredPosition = new Vector3(vInputVector.x * (imgBG.rectTransform.sizeDelta.x / 2),
                                                                    vInputVector.y * (imgBG.rectTransform.sizeDelta.y / 2));

           /* Debug.Log("vInputVector: " + vInputVector);*/
        }
    }

    public void OnPointerDown(PointerEventData eventData) // eventData: ���콺 ��ǥ ���� ���� 
    { // �̹��� �ȿ� ���콺 �����Ͱ� Ŭ�� �Ǿ��ٸ�
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData) // ���� 0���� 
    {
        vInputVector = Vector3.zero;
        //vInputVector = new Vector3(pos.x, pos.y, 0);

        imgJoystick.rectTransform.anchoredPosition = Vector3.zero;
    }

    //PlayerController ���� �Է� ���� �Ѱ��ֱ� ���� �Լ�
    public float GetHorizontalValue()
    {
        return vInputVector.x;
    }
    public float GetVerticalValue()
    {
        return vInputVector.y;
    }





}