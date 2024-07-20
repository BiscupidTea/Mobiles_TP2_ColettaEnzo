using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private TunnelsSo[] tunnelsSo;
    [SerializeField] private GameObject terrainPrefab;
    [SerializeField] private float spawnDistance;
    [SerializeField] private float eliminateDistance;

    public int FinalDistance;
    private int ActualTunnelSO = 0;
    private int ActualTunnelCoin = 0;
    private int ActualTunnelObstacle = 0;

    private List<SingleTerrain> terrainPool;
    
    void Start()
    {
        terrainPool = new List<SingleTerrain>();
        InitializeTerrainPool();

        SetNewValues();
    }

    private void InitializeTerrainPool()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(terrainPrefab);
            obj.SetActive(false);
            terrainPool.Add(obj.GetComponent<SingleTerrain>());
        }
    }

    private void SetNewValues()
    {
        player.GetComponent<PlayerMovement>().speed = tunnelsSo[ActualTunnelSO].newPlayerSpeed;

        foreach (SingleTerrain terrain in terrainPool)
        {
            terrain.obstacleSpeed = tunnelsSo[ActualTunnelSO].newObstacleSpeed;
        }
    }

    void Update()
    {
        if (Vector3.Distance(spawnPoint.position, player.transform.position) < spawnDistance)
        {
            SpawnNewTerrain();
            CheckTunnelState();
        }

        EliminateTerrain();
    }

    private void CheckTunnelState()
    {
        if (FinalDistance >= tunnelsSo[ActualTunnelSO].totalDistance && ActualTunnelSO + 1 < tunnelsSo.Length)
        {
            ActualTunnelSO++;
            SetNewValues();
        }
    }

    void SpawnNewTerrain()
    {
        ActualTunnelCoin++;
        ActualTunnelObstacle++;

        SingleTerrain newTerrain = GetTerrainFromPool();
        newTerrain.transform.position = spawnPoint.position;
        newTerrain.transform.rotation = spawnPoint.rotation;
        newTerrain.gameObject.SetActive(true);

        if (ActualTunnelObstacle >= tunnelsSo[ActualTunnelSO].totalTunnelsForObstacles)
        {
            newTerrain.SpawnObstacle();
            ActualTunnelObstacle = 0;
        }
        else if (ActualTunnelCoin >= tunnelsSo[ActualTunnelSO].totalTunnelsForCoin)
        {
            newTerrain.SpawnCoin();
            ActualTunnelCoin = 0;
        }
        
        spawnPoint.position = newTerrain.finalPoint.position;
    }

    private SingleTerrain GetTerrainFromPool()
    {
        foreach (SingleTerrain terrain in terrainPool)
        {
            if (!terrain.gameObject.activeSelf)
                return terrain;
        }

        SingleTerrain newTerrain = Instantiate(terrainPrefab).GetComponent<SingleTerrain>();
        terrainPool.Add(newTerrain);
        return newTerrain;
    }

    void EliminateTerrain()
    {
        for (int i = 0; i < terrainPool.Count; i++)
        {
            if (terrainPool[i].gameObject.activeSelf &&
                player.transform.position.z - terrainPool[i].transform.position.z > eliminateDistance)
            {
                terrainPool[i].gameObject.SetActive(false);
                FinalDistance++;
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
