using UnityEngine;

public class TunnelLogic : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform StartPoint;
    [SerializeField] private GameObject[] Tunnels;
    [SerializeField] private Transform[] PrefabObstacles;
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
        moveTunnel();
    }

    private void moveTunnel()
    {
        for (int i = 0; i < Tunnels.Length; i++)
        {
            if (IsTunnelEnd(Tunnels[i].transform))
            {
                Tunnels[i].transform.position = StartPoint.position;
                Tunnels[i].transform.rotation = StartPoint.rotation;
            }
            else
            {
                Tunnels[i].transform.Translate(-transform.forward * speed * Time.deltaTime);
            }
        }
    }

    private bool IsTunnelEnd(Transform tunnel)
    {
        if (tunnel.position.z <= endPoint.position.z)
        {
            StartPoint.Rotate(0,0,Random.Range(1,4)*90);
            return true;
        }
        else
        {
            return false;
        }
    }
}