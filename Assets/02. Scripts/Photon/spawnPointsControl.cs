using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPointsControl : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> spawnPointsList;
    // Start is called before the first frame update
    void Awake()
    {
        spawnPointsList = new List<GameObject>();

        Transform[] points = GetComponentsInChildren<Transform>();

        for(int i = 1; i < points.Length; i++)
        {
            spawnPointsList.Add(points[i].gameObject);
            points[i].GetComponent<pointControl>().pointId = i-1;
        }
    }
}
