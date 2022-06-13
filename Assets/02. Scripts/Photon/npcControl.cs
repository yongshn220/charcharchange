using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcControl : MonoBehaviour
{

    private npcManager npcManager;

    private int pointIndex = 0;

    void Start()
    {
        npcManager = GetComponentInParent<npcManager>();
    }

    void Update()
    {
        move();
    }

    public void move()
    {

    }
}
