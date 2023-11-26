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

    [SerializeField] private Image stateImage;
    [SerializeField] private Text stateText;

    [SerializeField] private Sprite textureState1;
    [SerializeField] private Sprite textureState2;
    [SerializeField] private Sprite textureState3;
    [SerializeField] private Sprite textureStateInvencible;

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

        disteanceText.text = tunnelLogic.FinalDistance.ToString();
        cointText.text = totalCoins.ToString();
        maxDistance = tunnelLogic.FinalDistance;

        actualInvencibleTime -= Time.deltaTime;

        if (actualInvencibleTime > 0)
        {
            stateImage.sprite = textureStateInvencible;
            stateText.text = "Shield";
        }
        else
        {
            setStateBar();
        }
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
        if (totalLives == 3)
        {
            stateImage.sprite = textureState1;
            stateText.text = "Perfect";
        }
        else if (totalLives == 2)
        {
            stateImage.sprite = textureState2;
            stateText.text = "Stable";
        }
        else
        {
            stateImage.sprite = textureState3;
            stateText.text = "Critical";
        }
    }
}
