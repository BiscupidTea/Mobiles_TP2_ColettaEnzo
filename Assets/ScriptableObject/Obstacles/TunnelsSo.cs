using UnityEngine;

[CreateAssetMenu(fileName = "New Tunnel Section", menuName = "Create Tunnel")]
public class TunnelsSo : ScriptableObject
{
    public GameObject TunnelPrefab;
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;

    public int totalTunnels;
    public int totalTunnelsForCoin;
    public int totalTunnelsForObstacles;
    public float newSpawnDistance;
    public float newSpeed;
}
