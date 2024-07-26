using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine;
using Event = Unity.Services.Analytics.Event;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;

    async void Start()
    {
        if (Instance == null)
        {
            Instance = this;

            await UnityServices.InitializeAsync();

            if (UnityServices.State == ServicesInitializationState.Initialized)
            {
                Debug.Log("Service initialized!");
            }

            AnalyticsService.Instance.StartDataCollection();

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void HandleReportCrashEvent()
    {
        AnalyticsCrashEvent crash = new AnalyticsCrashEvent();
        AnalyticsService.Instance.RecordEvent(crash);
        Debug.Log("HandleReportCrashEvent Recorded!");
    }

    public void HandleReportMaxDistanceMade(int maxDistance, string spaceShipName)
    {
        AnalyticsMaxDistanceMade maxDistanceMade = new AnalyticsMaxDistanceMade()
        {
            MaxDistance = maxDistance,
            SpaceShip_Name = spaceShipName
        };
        
        AnalyticsService.Instance.RecordEvent(maxDistanceMade);
        Debug.Log("HandleReportMaxDistanceMade Recorded!");
    }

    public void HandleReportPurchase(string spaceShipName, int value)
    {
        AnalyticPurchaseEvent purchaseSpaceShip = new AnalyticPurchaseEvent()
        {
            SpaceShip_Name = spaceShipName,
            SpaceShip_Value = value
        };
        
        AnalyticsService.Instance.RecordEvent(purchaseSpaceShip);
        Debug.Log("HandleReportPurchase Recorded!");
    }

    public void HandleTestEvent()
    {
        CustomEvent testEvent = new CustomEvent("Testeo")
        {
            { "ParametroTesteo", "EventTested!" }
        };
        AnalyticsService.Instance.RecordEvent(testEvent);
        Debug.Log("HandleTestEvent Recorded!");
    }
}

public class AnalyticsCrashEvent : Event
{
    public AnalyticsCrashEvent() : base("CrashReport")
    {
        
    }
}
