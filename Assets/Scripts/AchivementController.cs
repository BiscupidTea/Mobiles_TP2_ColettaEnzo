using GooglePlayGames;
using UnityEngine;

public class AchivementController : MonoBehaviour
{
    public static AchivementController Instance;
    private bool hasBeenAutenticated;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
            hasBeenAutenticated = true;

        FirstTime();
        Debug.Log("PlayGamesPlatform.Instance.localUser.authenticated : " + PlayGamesPlatform.Instance.localUser.authenticated);
#endif
    }

    public void ShowAchievements()
    {
#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
            PlayGamesPlatform.Instance.ShowAchievementsUI();
#endif
    }

    public void FirstTime()
    {
#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            if (!PlayerPrefs.HasKey("FirstTime"))
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI34T7ibsPEAIQAQ", 100f, succes =>
                  {
                      if (succes)
                      {
                          Debug.Log("Achievement unlocked: FirstTime");
                          PlayerPrefs.SetInt("FirstTime", 1);
                      }
                      else
                      {
                          Debug.LogError("Failed to unlock achievement: FirstTime");
                      }
                  });
            }
        }
#endif
    }

    public void Reach100m()
    {
#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            if (!PlayerPrefs.HasKey("FirstTime"))
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI34T7ibsPEAIQAg", 100f, succes =>
            {
                if (succes)
                {
                    Debug.Log("Achievement unlocked: Reach100m");
                    PlayerPrefs.SetInt("Reach100m", 1);
                }
                else
                {
                    Debug.LogError("Failed to unlock achievement: Reach100m");
                }
            });
            }
        }
#endif
    }

    public void BuyShip()
    {
#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            if (!PlayerPrefs.HasKey("FirstTime"))
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI34T7ibsPEAIQAw", 100f, succes =>
            {
                if (succes)
                {
                    Debug.Log("Achievement unlocked: BuyShip");
                    PlayerPrefs.SetInt("BuyShip", 1);
                }
                else
                {
                    Debug.LogError("Failed to unlock achievement: BuyShip");
                }
            });
            }
        }
#endif
    }

    public void accumulateCoins()
    {
#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            if (!PlayerPrefs.HasKey("FirstTime"))
            {
                PlayGamesPlatform.Instance.ReportProgress("CgkI34T7ibsPEAIQBA", 100f, succes =>
                 {
                     if (succes)
                     {
                         Debug.Log("Achievement unlocked: accumulateCoins");
                         PlayerPrefs.SetInt("accumulateCoins", 1);
                     }
                     else
                     {
                         Debug.LogError("Failed to unlock achievement: accumulateCoins");
                     }
                 });
            }
        }
#endif
    }
}
