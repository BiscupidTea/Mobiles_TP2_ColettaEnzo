using System.Threading;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LooseScreenDistance : MonoBehaviour
{
    [SerializeField] private PlayerSo player;
    [SerializeField] private Text textDistance;
    [SerializeField] private Text maxTextDistance;
    [SerializeField] private Text totalCoins;
    [SerializeField] private GameObject AdsGameObject;

    private bool canWatchAd = true;

    private void Start()
    {
        AdsGameObject.SetActive(true);
        textDistance.text = player.distance.ToString();
        maxTextDistance.text = player.maxDistance.ToString();
        totalCoins.text = player.moneyToCharge.ToString();
    }

    public void ShowAdd()
    {
        if (PlayerPrefs.HasKey("Deaths"))
        {
            int deaths = PlayerPrefs.GetInt("Deaths");
            if (deaths >= 3)
            {
                UnityAdsManager.Instance.ShowNonRewardedAd();
                deaths = 0;
            }
            else
            {
                deaths++;
            }
            Debug.Log(deaths);
            PlayerPrefs.SetInt("Deaths", deaths);
        }
        else
        {
            PlayerPrefs.SetInt("Deaths", 0);
        }
    }

    public void AssingValuesToPlayerStats()
    {
        player.totalMoney = player.totalMoney + player.moneyToCharge;
        player.moneyToCharge = 0;

        if (player.totalMoney > 20)
        {
            AchivementController.Instance.AccumulateCoins();
        }

        DataLoader.Instance.SaveProgress();
        AnalyticsManager.Instance.HandleReportMaxDistanceMade((int)player.maxDistance, player.pickedSpaceShip);
    }

    public void DuplicatePlayerMoney()
    {
        if (canWatchAd)
        {
            player.moneyToCharge = player.moneyToCharge * 2;
            totalCoins.text = player.moneyToCharge.ToString();
            canWatchAd = false;
            AdsGameObject.SetActive(false);
        }
    }
}