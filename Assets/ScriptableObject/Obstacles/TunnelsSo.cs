using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Tunnel Section", menuName = "Create Tunnel")]
public class TunnelsSo : ScriptableObject
{
    public int totalDistance;
    public int totalTunnelsForCoin;
    public int totalTunnelsForObstacles;
    public float newPlayerSpeed;
    public float newObstacleSpeed;
}
