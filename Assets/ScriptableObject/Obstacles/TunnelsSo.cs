using UnityEngine;

[CreateAssetMenu(fileName = "New Tunnel Section", menuName = "Create Tunnel")]
public class TunnelsSo : ScriptableObject
{
    public GameObject clearTunnelPrefab;
    public GameObject CoinTunnelPrefab;
    public GameObject[] TunnelPrefabs;

    public int totalTunnels;
    public int totalTunnelsForCoin;
    public int totalTunnelsForObstacles;
    public float newSpawnDistance;
    public float newSpeed;
}
