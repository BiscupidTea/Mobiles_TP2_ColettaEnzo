using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TunnelLogic : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private List<GameObject> terrainList;

    [Header("Clear Prefabs")]
    [SerializeField] private GameObject clearSmallprefabTerrains;
    [SerializeField] private GameObject clearMediumprefabTerrains;
    [SerializeField] private GameObject clearBigPrefabTerrains;
    [SerializeField] private GameObject bigMediumPrefabTerrains;
    [SerializeField] private GameObject mediumSmallPrefabTerrains;

    [Header("List Prefabs")]
    [SerializeField] private GameObject[] smallPrefabTerrains;
    [SerializeField] private GameObject[] mediumPrefabTerrains;
    [SerializeField] private GameObject[] bigPrefabTerrains;

    [Header("Entities Data")]
    [SerializeField] private float spawnDistance;
    [SerializeField] private float eliminateDistance;

    [Header("Game Data")]
    [SerializeField] private int totalBigDistance;
    [SerializeField] private int totalMediumDistance;

    private int totalPass;
    private int ActualDistance;

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
        GameObject terrainPrefab = smallPrefabTerrains[Random.Range(0, smallPrefabTerrains.Length)];
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
                ActualDistance++;
                break;
            }
        }
    }
}