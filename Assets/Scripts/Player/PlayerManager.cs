using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerSo player;
    [SerializeField] private TunnelLogic tunnelLogic;
    [SerializeField] private Transform parentPosition;
    [SerializeField] private UnityEvent finishGame;
    [SerializeField] private float invencibleTime;
    private float totalCoins;
    private float maxDistance;

    [SerializeField] private Text disteanceText;
    [SerializeField] private Text cointText;

    [SerializeField] private Image textureState1;
    [SerializeField] private Image textureState2;
    [SerializeField] private Image textureState3;

    [SerializeField] private GameObject particle1;
    [SerializeField] private GameObject particle2;

    private int totalLives;
    private float actualInvencibleTime;

    private void Start()
    {
        Instantiate(player.SelectedSpaceShip.gameObject, parentPosition);
        maxDistance = player.maxDistance;
        totalCoins = player.totalMoney;
        cointText.text = totalCoins.ToString();
        totalLives = player.totalLives;
    }

    private void Update()
    {
        if (totalLives <= 0)
        {

            if (maxDistance > PlayerPrefs.GetFloat("distance"))
            {
                player.maxDistance = maxDistance;
                PlayerPrefs.SetFloat("distance", maxDistance);
                Debug.Log(PlayerPrefs.GetFloat("distance"));
            }

            if (totalCoins > PlayerPrefs.GetFloat("money"))
            {
                player.totalMoney = totalCoins;
                PlayerPrefs.SetFloat("money", totalCoins);
                Debug.Log(PlayerPrefs.GetFloat("distance"));
            }

            player.distance = maxDistance;

            finishGame.Invoke();
        }

        disteanceText.text = tunnelLogic.FinalDistance.ToString() + "m";
        cointText.text = totalCoins.ToString();
        maxDistance = tunnelLogic.FinalDistance;

        actualInvencibleTime -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            collision.gameObject.GetComponent<Obstacle>().DestroyObstacle();

            if (actualInvencibleTime <= 0)
            {
                totalLives--;
                actualInvencibleTime = invencibleTime;
                setStateBar();

                if (SystemInfo.supportsVibration)
                {
                    Handheld.Vibrate();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            totalCoins++;
            other.gameObject.GetComponent<Coin>().DestroyObstacle();
        }
    }

    private void setStateBar()
    {
        if (totalLives == 2)
        {
            textureState1.enabled = false;
            particle1.SetActive(true);
        }
        else
        {
            textureState2.enabled = false;
            particle2.SetActive(true);
        }
    }
}
