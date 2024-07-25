using GooglePlayGames;
using UnityEngine;

public class AchivementController : MonoBehaviour
{
    public static AchivementController Instance;

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
#if UNITY_ANDROID
            FirstTime();
            Debug.Log("PlayGamesPlatform.Instance.localUser.authenticated : " +
                      PlayGamesPlatform.Instance.localUser.authenticated);
#endif
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
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
            PlayGamesPlatform.Instance.ReportProgress("CgkI34T7ibsPEAIQAQ", 100f, succes => { });
        }
#endif
    }

    public void Reach100m()
    {
#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportProgress("CgkI34T7ibsPEAIQAg", 100f, succes => { });
        }
#endif
    }

    public void BuyShip()
    {
#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportProgress("CgkI34T7ibsPEAIQAw", 100f, succes => { });
        }
#endif
    }

    public void AccumulateCoins()
    {
#if UNITY_ANDROID
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportProgress("CgkI34T7ibsPEAIQBA", 100f, succes => { });
        }
#endif
    }
}