using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcControl : MonoBehaviour
{

    private npcManager npcManager;

    private GameObject targetPoint;

    void Start()
    {
        npcManager = GetComponentInParent<npcManager>();

        targetPoint = npcManager.spawnPoints.spawnPointsList[0];
    }

    void Update()
    {
        move();
    }

    public void move()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Point"))
        {

        }
    }
}
