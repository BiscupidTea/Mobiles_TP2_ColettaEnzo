using UnityEngine;
using UnityEngine.UI;

public class LooseScreenDistance : MonoBehaviour
{
    [SerializeField] private PlayerSo player;
    [SerializeField] private Text TextDistance;
    [SerializeField] private Text MaxTextDistance;

    private void Start()
    {
        string distance = "Total Distance = " + player.distance.ToString();
        TextDistance.text = distance;

        distance = "Record Distance = " + player.maxDistance.ToString();
        MaxTextDistance.text = distance;
    }
}
