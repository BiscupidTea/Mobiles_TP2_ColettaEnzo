using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancesCallScript : MonoBehaviour
{
    public void CallAddIntersical()
    {
        UnityAdsManager.Instance.ShowNonRewardedAd();
    }

    public void CallAddReward()
    {
        UnityAdsManager.Instance.ShowRewardedAd();
    }

    public void ShowAchievents()
    {
        AchivementController.Instance.ShowAchievements();
    }
}
