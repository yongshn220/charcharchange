using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcControl : MonoBehaviour
{

    private npcManager npcManager;

    [SerializeField]
    private GameObject targetPoint;

    [SerializeField]
    public int targetId;
    public int targetCount;

    public float moveSpeed = 4.0f;
    void Start()
    {
        npcManager = GetComponentInParent<npcManager>();

        UpdateTarget();
    }
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.transform.position, step);
        transform.LookAt(targetPoint.transform);
    }

    IEnumerator OnTriggerEnter(Collider other) {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("trigger");
        if(other.CompareTag("Point"))
        {
            if(other.gameObject.GetComponent<pointControl>().pointId == targetId)
            {
                UpdateTarget();
            }
        }
    }

    private void UpdateTarget()
    {
        targetId = targetId + 1;
        if(targetId > targetCount - 1)
        {
            targetId = 0;
        }
        targetPoint = npcManager.spawnPoints.spawnPointsList[targetId];
    }
}
