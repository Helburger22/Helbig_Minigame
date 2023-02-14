using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawnManager : MonoBehaviour
{
    public GameObject[] sharkPrefabs;
    public float spawnRangeX = 20;
    public float spawnPosZ = 20;
    public float startDelay = 2;
    public float spawnInterval = 1.5f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        // spawns shark at time interval set by variables
        InvokeRepeating("SpawnShark", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        //stops spawning once player object is destroyed
        if (player == null)
        {
            CancelInvoke("SpawnShark");
        }

    }
    //funtion that randomizes spawn placement of sharks
    void SpawnShark()
    {
        int sharkIndex = Random.Range(0, sharkPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        Instantiate(sharkPrefabs[sharkIndex], spawnPos, sharkPrefabs[sharkIndex].transform.rotation);
    }
}
