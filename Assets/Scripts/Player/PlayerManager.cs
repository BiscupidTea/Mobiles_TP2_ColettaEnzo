using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using EZCameraShake;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerSo player;
    [SerializeField] private ObstaclesManager obstaclesManager;
    [SerializeField] private Transform parentPosition;
    [SerializeField] private float invencibleTime;
    [SerializeField] private AudioSource crashSound;
    private int totalCoins;
    private float maxDistance;
    public IMediator mediator;
    private List<IObserver> observers = new();

    [SerializeField] private Text disteanceText;
    [SerializeField] private Text cointText;

    private int totalLives;
    private float actualInvencibleTime;

    private void Start()
    {
        Time.timeScale = 0;
        Instantiate(DataLoader.Instance.GetCurrentPlayerSpaceShip().prefab, parentPosition);
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
            player.moneyToCharge = totalCoins;
            player.distance = maxDistance;
            if (player.distance > player.maxDistance)
            {
                player.maxDistance = player.distance;
            }

            mediator.NotifyPlayerDeath();
        }

        disteanceText.text = obstaclesManager.FinalDistance.ToString() + "m";
        cointText.text = totalCoins.ToString();
        maxDistance = obstaclesManager.FinalDistance;

        actualInvencibleTime -= Time.deltaTime;

        if (maxDistance > 100)
        {
            AchivementController.Instance.Reach100m();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Obstacle>())
        {
            collision.gameObject.GetComponent<Obstacle>().DestroyObstacle();

            if (actualInvencibleTime <= 0)
            {
                crashSound.Play();
                totalLives--;
                UpdateVisualLife(totalLives);
                actualInvencibleTime = invencibleTime;

                CameraShaker.Instance.ShakeOnce(6, 6, 0.5f, 0.5f);

                if (SystemInfo.supportsVibration)
                {
                    Handheld.Vibrate();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Coin>())
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