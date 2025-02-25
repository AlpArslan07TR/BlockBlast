using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs;  
    public Transform[] spawnPoints;

    private Dictionary<Transform, GameObject> activeBlocks = new Dictionary<Transform, GameObject>();
    void Start()
    {
        SpawnBlocks();
    }

    public void SpawnBlocks()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (!activeBlocks.ContainsKey(spawnPoint) || activeBlocks[spawnPoint] == null)
            {
                SpawnBlockAt(spawnPoint);
            }
        }
    }
    public void SpawnBlockAt(Transform spawnPoint)
    {
        int randomIndex = Random.Range(0, blockPrefabs.Length);
        GameObject spawnedBlock = Instantiate(blockPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);

        
        spawnedBlock.GetComponent<BlockDragHandler>().SetSpawnPoint(spawnPoint);

        activeBlocks[spawnPoint] = spawnedBlock;
    }
}
