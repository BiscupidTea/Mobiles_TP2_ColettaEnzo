using UnityEngine;

public class AchivementController : MonoBehaviour
{
    public void FirstTime()
    {
        Social.ReportProgress("CgkI34T7ibsPEAIQAQ", 100, (bool success) => { });
    }

    public void Reach100m()
    {
        Social.ReportProgress("CgkI34T7ibsPEAIQAg", 100, (bool success) => { });
    }

    public void BuyShip()
    {
        Social.ReportProgress("CgkI34T7ibsPEAIQAw", 100, (bool success) => { });
    }

    public void accumulateCoins()
    {
        Social.ReportProgress("CgkI34T7ibsPEAIQBA", 100, (bool success) => { });
    }
}
