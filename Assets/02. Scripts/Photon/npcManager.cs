using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class npcManager : MonoBehaviour
{

    public spawnPointsControl spawnPoints;
    public cshGameManager gameManager;
    private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = cshGameManager.instance;
        spawnNpc();
    }

    private void spawnNpc()
    {
        foreach(var point in spawnPoints.spawnPointsList)
        {

            int id = Random.Range(0, 10);

            GameObject npc = Instantiate(gameManager.spawnPrefabs[0], transform);
            npc.GetComponent<npcControl>().targetId = point.GetComponent<pointControl>().pointId;
            npc.GetComponent<npcControl>().targetCount = spawnPoints.spawnPointsList.Count;
            npc.transform.position = point.transform.position;
        }
    }
}
