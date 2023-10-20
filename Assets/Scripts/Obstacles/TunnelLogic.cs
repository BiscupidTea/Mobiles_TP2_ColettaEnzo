using UnityEngine;

public class TunnelLogic : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform StartPoint;

    [SerializeField] private tunnel[] Tunnels;

    [SerializeField] private GameObject defaultTunnel;
    [SerializeField] private GameObject[] PrefabObstacles;

    [SerializeField] private float speed;
    [SerializeField] private int tunnelsBeforeObstacle;
    private int tunnelCounter;

    private void FixedUpdate()
    {
        moveTunnel();
    }

    private void moveTunnel()
    {
        for (int i = 0; i < Tunnels.Length; i++)
        {
            if (IsTunnelEnd(Tunnels[i].tunnelTransform))
            {
                Tunnels[i].tunnelTransform.position = StartPoint.position;
                Tunnels[i].tunnelTransform.rotation = StartPoint.rotation;
                tunnelCounter++;

                if (tunnelCounter >= tunnelsBeforeObstacle)
                {
                    Tunnels[i].tunnelAsset = PrefabObstacles[Random.Range(0, PrefabObstacles.Length)];
                    tunnelCounter = 0;
                }
                else
                {
                    Tunnels[i].tunnelAsset = defaultTunnel;
                    StartPoint.Rotate(0, 0, Random.Range(1, 4) * 90);
                }

            }
            else
            {
                Tunnels[i].tunnelTransform.Translate(-transform.forward * speed * Time.deltaTime);
            }
        }
    }

    private bool IsTunnelEnd(Transform tunnel)
    {
        if (tunnel.position.z <= endPoint.position.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

[System.Serializable]
public class tunnel
{
    public Transform tunnelTransform;
    public GameObject tunnelAsset;
}