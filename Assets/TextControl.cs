using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class TextControl : MonoBehaviour
{
    cshGameManager gm;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        gm = cshGameManager.instance;
        text.text = PhotonNetwork.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - gm.cvCamera.transform.position);
    }
}
