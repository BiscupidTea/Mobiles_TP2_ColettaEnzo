using System.Collections.Generic;
using UnityEngine;

public class TunnelLogic : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;

    [Header("Game Data")]
    [SerializeField] private TunnelsSo[] tunnelsSo;
    [SerializeField] private List<GameObject> terrainList;

    [Header("Entities Data")]
    [SerializeField] private float spawnDistance;
    [SerializeField] private float eliminateDistance;

    private int TotalTunnelsPassed;
    public int FinalDistance;
    private int ActualTunnel = 0;

    private int ActualTunnelCoin = 0;
    private int ActualTunnelObstacle = 0;

    void Update()
    {
        if (TotalTunnelsPassed >= tunnelsSo[ActualTunnel].totalTunnels)
        {
            ActualTunnel++;
            if (ActualTunnel >= tunnelsSo.Length)
            {
                ActualTunnel--;
            }

            spawnDistance = tunnelsSo[ActualTunnel].newSpawnDistance;
            player.GetComponent<PlayerMovement>().speed = tunnelsSo[ActualTunnel].newSpeed;
            TotalTunnelsPassed = 0;
        }
        else
        {
            if (Vector3.Distance(spawnPoint.position, player.transform.position) < spawnDistance)
            {
                ActualTunnelCoin++;
                ActualTunnelObstacle++;
                SpawnNewTerrain(tunnelsSo[ActualTunnel]);
            }
        }

        EliminateTerrain();
    }

    void SpawnNewTerrain(TunnelsSo ActualTunnel)
    {
        GameObject terrainPrefab;

        if (ActualTunnelObstacle >= ActualTunnel.totalTunnelsForObstacles)
        {
            terrainPrefab = ActualTunnel.TunnelPrefabs[Random.Range(0, ActualTunnel.TunnelPrefabs.Length)];
            ActualTunnelObstacle = 0;
        }
        else if (ActualTunnelCoin >= ActualTunnel.totalTunnelsForCoin)
        {
            terrainPrefab = ActualTunnel.CoinTunnelPrefab;
            ActualTunnelCoin = 0;
        }
        else
        {
            terrainPrefab = ActualTunnel.clearTunnelPrefab;
        }

        GameObject newTerrain = Instantiate(terrainPrefab, spawnPoint.position, spawnPoint.rotation);
        spawnPoint.position = newTerrain.GetComponent<SingleTerrain>().finalPoint.position;
        terrainList.Add(newTerrain);
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
        Gizmos.DrawLine(player.transform.position, player.transform.position + player.transform.forward * spawnDistance);
        Gizmos.DrawLine(player.transform.position, player.transform.position + -player.transform.forward * eliminateDistance);
    }
}