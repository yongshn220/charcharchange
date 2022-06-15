using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextControl : MonoBehaviour
{
    cshGameManager gm;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        gm = cshGameManager.instance;
        text.text = PlayerPrefs.GetString("userId");
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - gm.cvCamera.transform.position);
    }
}
