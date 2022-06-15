using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class npcControl : MonoBehaviour
{

    private npcManager npcManager;

    public int id;

    [SerializeField]
    private GameObject targetPoint;

    [SerializeField]
    public int targetId;
    public int targetCount;

    public float moveSpeed = 4.0f;

    private PhotonView pv;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        npcManager = GetComponentInParent<npcManager>();
        Init();
    }
    void Update()
    {
        if(!PhotonNetwork.IsMasterClient) { return; }
        Move();
    }

    void Init()
    {
        if(!PhotonNetwork.IsMasterClient) { return; }
        moveSpeed = Random.Range(4,6);
        UpdateTarget();
    }

    public void Move()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.transform.position, step);
        transform.LookAt(targetPoint.transform);
    }

    IEnumerator OnTriggerEnter(Collider other) {

        yield return new WaitForSeconds(1.0f);

        if(PhotonNetwork.IsMasterClient)
        {

            if(other.CompareTag("Point"))
            {
                if(other.gameObject.GetComponent<pointControl>().pointId == targetId)
                {
                    UpdateTarget();
                }
            }
        }
    }

    private void UpdateTarget()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            targetId = targetId + 1;
            if(targetId > targetCount - 1)
            {
                targetId = 0;
            }
            targetPoint = npcManager.spawnPoints.spawnPointsList[targetId];
        }
    }
}
