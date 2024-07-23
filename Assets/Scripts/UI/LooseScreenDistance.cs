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

    public void AssingValuesToPlayerStats()
    {
        player.totalMoney = player.moneyToCharge;
        player.moneyToCharge = 0;
        
        if (player.totalMoney > 20)
        {
            AchivementController.Instance.AccumulateCoins();
        }
        
        DataLoader.Instance.OpenSave(true);
    }

    public void DuplicatePlayerMoney()
    {
        if (canWatchAd)
        {
            UnityAdsManager.Instance.ShowRewardedAd();
            player.moneyToCharge = player.moneyToCharge * 2;
            totalCoins.text = player.moneyToCharge.ToString();
            canWatchAd = false;
            AdsGameObject.SetActive(false);
        }
    }
}
