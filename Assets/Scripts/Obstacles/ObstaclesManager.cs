using System.Collections.Generic;
using UnityEngine;

public class ObstaclesManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform spawnPoint;

    [Header("Game Data")] [SerializeField] private TunnelsSo[] tunnelsSo;
    [SerializeField] private GameObject terrainPrefab;

    [Header("Entities Data")] [SerializeField] private float spawnDistance;
    [SerializeField] private float eliminateDistance;
    
    public int FinalDistance;
    private int ActualTunnelSO = 0;

    private int ActualTunnelCoin = 0;
    private int ActualTunnelObstacle = 0;

    private Queue<GameObject> terrainPool;
    private List<GameObject> activeTerrains;

    void Start()
    {
        terrainPool = new Queue<GameObject>();
        activeTerrains = new List<GameObject>();
        
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(terrainPrefab);
            obj.SetActive(false);
            terrainPool.Enqueue(obj);
        }

        SetNewValues();
    }

    private void SetNewValues()
    {
        player.GetComponent<PlayerMovement>().speed = tunnelsSo[ActualTunnelSO].newPlayerSpeed;

        foreach (GameObject o in terrainPool)
        {
            o.GetComponent<SingleTerrain>().obstacleSpeed = tunnelsSo[ActualTunnelSO].newObstacleSpeed;
        }
    }

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

        GameObject newTerrain;
        if (terrainPool.Count > 0)
        {
            newTerrain = terrainPool.Dequeue();
        }
        else
        {
            newTerrain = Instantiate(terrainPrefab);
        }

        newTerrain.transform.position = spawnPoint.position;
        newTerrain.transform.rotation = spawnPoint.rotation;
        newTerrain.SetActive(true);

        SingleTerrain tunnelPrefab = newTerrain.GetComponent<SingleTerrain>();

        if (ActualTunnelObstacle >= tunnelsSo[ActualTunnelSO].totalTunnelsForObstacles)
        {
            tunnelPrefab.SpawnObstacle();
            ActualTunnelObstacle = 0;
        }
        else if (ActualTunnelCoin >= tunnelsSo[ActualTunnelSO].totalTunnelsForCoin)
        {
            tunnelPrefab.SpawnCoin();
            ActualTunnelCoin = 0;
        }

        spawnPoint.position = tunnelPrefab.finalPoint.position;
        activeTerrains.Add(newTerrain);

        EliminateTerrain();
    }

    void EliminateTerrain()
    {
        for (int i = 0; i < activeTerrains.Count; i++)
        {
            if (player.transform.position.z - activeTerrains[i].transform.position.z > eliminateDistance)
            {
                GameObject obj = activeTerrains[i];
                obj.SetActive(false);
                terrainPool.Enqueue(obj);
                activeTerrains.RemoveAt(i);
                
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