using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashDetection : MonoBehaviour
{
    private const string GameStateKey = "GameState";
    private const string RunningState = "Running";
    private const string ClosedState = "Closed";

    private static bool instanceExists = false;
    void Awake()
    {
        if (instanceExists)
        {
            Destroy(gameObject);
            return;
        }

        instanceExists = true;
        DontDestroyOnLoad(gameObject);

        CheckGameState();

        PlayerPrefs.SetString(GameStateKey, RunningState);
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetString(GameStateKey, ClosedState);
        PlayerPrefs.Save();
    }

    void CheckGameState()
    {
        if (PlayerPrefs.HasKey(GameStateKey))
        {
            string lastState = PlayerPrefs.GetString(GameStateKey);
            if (lastState == RunningState)
            {
                AnalyticsManager.Instance.HandleReportCrashEvent();
            }
        }
    }
}
