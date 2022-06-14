using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testNPCContrl : MonoBehaviour
{
    private Vector3 vec;
    // Update is called once per frame
    void Update()
    {
        vec = transform.localPosition; 
        vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * 5;  
        vec.z += Input.GetAxis("Vertical") * Time.deltaTime * 5;  
        transform.localPosition = vec;  
    }
}
