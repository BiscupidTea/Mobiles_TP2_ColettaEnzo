using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine;

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

    public void HandleTestEvent()
    {
        CustomEvent testEvent = new CustomEvent("Testeo")
        {
            { "ParametroTesteo", "EventTested!" }
        };
        AnalyticsService.Instance.RecordEvent(testEvent);
        Debug.Log("Parameter tested!");
    }
}