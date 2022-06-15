using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class npcManager : MonoBehaviourPun
{

    public spawnPointsControl spawnPoints;
    public cshGameManager gameManager;
    private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = cshGameManager.instance;
        if(PhotonNetwork.IsMasterClient)
        {
            spawnNpc();
        }
    }

    private void spawnNpc()
    {
        foreach(var point in spawnPoints.spawnPointsList)
        {

            int id = Random.Range(0, 10);

            GameObject npc = PhotonNetwork.Instantiate(gameManager.spawnPrefabs[0].name, transform.position, Quaternion.identity, 0);
            npc.transform.SetParent(transform);
            npc.GetComponent<npcControl>().targetId = point.GetComponent<pointControl>().pointId;
            npc.GetComponent<npcControl>().targetCount = spawnPoints.spawnPointsList.Count;
            npc.transform.position = point.transform.position;
        }
    }
}
