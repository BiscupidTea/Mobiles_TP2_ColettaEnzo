using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TunnelLogic : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private List<GameObject> terrainList;
    [SerializeField] private GameObject[] prefabTerrains;

    [SerializeField] private float spawnDistance;
    [SerializeField] private float eliminateDistance;

    void Update()
    {
        if (Vector3.Distance(spawnPoint.position, player.position) < spawnDistance)
        {
            SpawnNewTerrain();
        }

        EliminateTerrain();
    }

    void SpawnNewTerrain()
    {
        GameObject terrainPrefab = prefabTerrains[Random.Range(0, prefabTerrains.Length)];
        GameObject newTerrain = Instantiate(terrainPrefab, spawnPoint.position, spawnPoint.rotation);
        spawnPoint.position = newTerrain.GetComponent<SingleTerrain>().finalPoint.position;
        terrainList.Add(newTerrain);
    }

    void EliminateTerrain()
    {
        for (int i = 0; i < terrainList.Count; i++)
        {
            if (player.position.z - terrainList[i].transform.position.z > eliminateDistance)
            {
                Destroy(terrainList[i]);
                terrainList.RemoveAt(i);
                i--;
            }
        }
    }
}