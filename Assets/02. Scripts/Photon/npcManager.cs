using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class npcManager : MonoBehaviour
{

    public spawnPointsControl spawnPoints;
    public GameObject[] npcs;

    private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnNpc();
    }

    private void spawnNpc()
    {
        foreach(var point in spawnPoints.spawnPointsList)
        {
            int id = Random.Range(0, 2);
            GameObject npc = Instantiate(npcs[id]);

            npc.transform.position = point.transform.position;
        }
    }
}
