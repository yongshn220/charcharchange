using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cshGameManager : MonoBehaviour
{

    public GameObject[] spawns;
    public GameObject[] spawnPrefabs;
    public int playerId;
    public Mesh[] meshes;

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("spawn");

        foreach (GameObject spawn in spawns)
        {
            int index = Random.Range(0, spawnPrefabs.Length);

            GameObject x = Instantiate(spawnPrefabs[index], spawn.transform.position, spawn.transform.rotation);
            x.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(1, 360), 0));
        }

        playerId = Random.Range(0, spawnPrefabs.Length) + 1;

        Debug.Log("gameManager playerId :" +playerId);
     
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
