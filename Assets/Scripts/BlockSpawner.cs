using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs;  
    public Transform[] spawnPoint;    

    void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            int randomIndex = Random.Range(0, blockPrefabs.Length);

            
            GameObject spawnedBlock = Instantiate(blockPrefabs[randomIndex], spawnPoint[i].position, Quaternion.identity);

            
            spawnedBlock.transform.SetParent(spawnPoint[i]);
        }
    }
}
