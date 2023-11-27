using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerSo player;
    [SerializeField] private TunnelLogic tunnelLogic;
    [SerializeField] private Transform parentPosition;
    [SerializeField] private float invencibleTime;
    private float totalCoins;
    private float maxDistance;
    public IMediator mediator;
    private List<IObserver> observers = new();

    [SerializeField] private Text disteanceText;
    [SerializeField] private Text cointText;

    private int totalLives;
    private float actualInvencibleTime;
    private AchivementController achivement;

    private void Start()
    {
        Time.timeScale = 0;
        Instantiate(player.SelectedSpaceShip.gameObject, parentPosition);
        maxDistance = player.maxDistance;
        totalCoins = player.totalMoney;
        cointText.text = totalCoins.ToString();
        totalLives = player.totalLives;
        UpdateVisualLife(totalLives);
    }

    private void Update()
    {
        if (totalLives <= 0)
        {

            if (maxDistance > PlayerPrefs.GetFloat("distance"))
            {
                player.maxDistance = maxDistance;
                PlayerPrefs.SetFloat("distance", maxDistance);
            }

            player.totalMoney = totalCoins;
            PlayerPrefs.SetFloat("money", totalCoins);

            player.distance = maxDistance;
            mediator.NotifyPlayerDeath();
        }

        disteanceText.text = tunnelLogic.FinalDistance.ToString() + "m";
        cointText.text = totalCoins.ToString();
        maxDistance = tunnelLogic.FinalDistance;

        actualInvencibleTime -= Time.deltaTime;

        if (maxDistance > 100)
        {
            achivement.Reach100m();
        }

        if (totalCoins > 20)
        {
            achivement.accumulateCoins();
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
                UpdateVisualLife(totalLives);
                actualInvencibleTime = invencibleTime;

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

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void UpdateVisualLife(int newLife)
    {
        totalLives = newLife;
        NotifyObservers();
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.UpdateObserver(totalLives);
        }
    }
}
