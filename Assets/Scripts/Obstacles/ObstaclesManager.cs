using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;

    [Header("Game Data")] [SerializeField] private TunnelsSo[] tunnelsSo;
    [SerializeField] private List<GameObject> terrainList;

    [Header("Entities Data")] [SerializeField]
    private float spawnDistance;

    [SerializeField] private float eliminateDistance;

    private int TotalTunnelsPassed;
    public int FinalDistance;
    private int ActualTunnelSO = 0;

    private int ActualTunnelCoin = 0;
    private int ActualTunnelObstacle = 0;

    void Update()
    {
        if (Vector3.Distance(spawnPoint.position, player.transform.position) < spawnDistance)
        {
            SpawnNewTerrain();
            CheckTunnelState();
        }
    }

    private void CheckTunnelState()
    {
        if (ActualTunnelObstacle > tunnelsSo[ActualTunnelSO].totalTunnels)
        {
            if (tunnelsSo[ActualTunnelSO + 1] != null)
            {
                ActualTunnelSO++;
            }
        }
    }

    void SpawnNewTerrain()
    {
        ActualTunnelCoin++;
        ActualTunnelObstacle++;
        
        GameObject newTerrain =
            Instantiate(tunnelsSo[ActualTunnelSO].TunnelPrefab, spawnPoint.position, spawnPoint.rotation);
        SingleTerrain tunnelPrefab = newTerrain.GetComponent<SingleTerrain>();

        if (ActualTunnelObstacle >= tunnelsSo[ActualTunnelSO].totalTunnelsForObstacles)
        {
            tunnelPrefab.SpawnObstacle(tunnelsSo[ActualTunnelSO].obstaclePrefab);
            newTerrain.name = newTerrain.name + " Obstacle";
            ActualTunnelObstacle = 0;
        }
        else if (ActualTunnelCoin >= tunnelsSo[ActualTunnelSO].totalTunnelsForCoin)
        {
            tunnelPrefab.SpawnCoin(tunnelsSo[ActualTunnelSO].coinPrefab);
            newTerrain.name = newTerrain.name + " Coin";
            ActualTunnelCoin = 0;
        }


        spawnPoint.position = tunnelPrefab.finalPoint.position;
        EliminateTerrain();
    }

    void EliminateTerrain()
    {
        for (int i = 0; i < terrainList.Count; i++)
        {
            if (player.transform.position.z - terrainList[i].transform.position.z > eliminateDistance)
            {
                Destroy(terrainList[i]);
                terrainList.RemoveAt(i);

                TotalTunnelsPassed++;
                FinalDistance++;
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(player.transform.position,
            player.transform.position + player.transform.forward * spawnDistance);
        Gizmos.DrawLine(player.transform.position,
            player.transform.position + -player.transform.forward * eliminateDistance);
    }
}