using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    private const string packName = "com.coletta";
    private const string className = packName + ".colettalogger";
    private int currentLogType;
    private string[] tempArray;
    [SerializeField] private GameObject textPanel;
    [SerializeField] private GameObject textObject;
    [SerializeField] private List<GameObject> createdLogs;

#if UNITY_ANDROID

    private AndroidJavaClass _pluginUnityClass;
    private AndroidJavaObject _pluginInstance;
    private AndroidJavaObject _unityActivity;
    public TextMeshProUGUI _label;

    private void Start()
    {
        currentLogType = 0;
        if (Application.platform == RuntimePlatform.Android)
        {
            _pluginUnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _unityActivity = _pluginUnityClass.GetStatic<AndroidJavaObject>("currentActivity");
            _pluginInstance = new AndroidJavaObject(className);
            if (_pluginInstance == null)
            {
                Debug.Log("Plugin Instance is NULL");
                return;
            }

            Application.logMessageReceived += Application_logMessageReceived;
            _pluginInstance.CallStatic("reciveUnityActivity", _unityActivity);

            Debug.Log("Unity Java Class Created");
        }
    }

    private void Application_logMessageReceived(string condition, string stacktrace, LogType type)
    {
        switch (type)
        {
            case LogType.Error:
                _pluginInstance.Call("SendError", condition);
                SendToWrite(condition, LogType.Error);
                break;
            case LogType.Assert:
                Debug.Log("AssertLog");
                break;
            case LogType.Warning:
                _pluginInstance.Call("SendWarning", condition);
                SendToWrite(condition, LogType.Warning);
                break;
            case LogType.Log:
                _pluginInstance.Call("SendLog", condition);
                SendToWrite(condition, LogType.Log);
                break;
            case LogType.Exception:
                Debug.Log("ExceptionLog");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public void RunPlugin()
    {
        Debug.Log("Runing Plugin");
        if (Application.platform == RuntimePlatform.Android)
        {
            _label.text = _pluginInstance.Call<string>("GetLOGTAG");
        }
    }

    public void CreateLog(string message)
    {
        string data = "";
        LogType currentLog = LogType.Log;
        switch (currentLogType)
        {
            case 0:
                Debug.Log("Debug Log - " + message);
                currentLog = LogType.Log;
                data = "Debug Log - " + message;
                break;
            case 1:
                Debug.Log("Warning Log - " + message);
                currentLog = LogType.Warning;
                data = "Warning Log - " + message;
                break;
            case 2:
                Debug.Log("Error Log - " + message);
                currentLog = LogType.Error;
                data = "Error Log - " + message;
                break;
        }

        currentLogType++;
        if (currentLogType >= 3)
            currentLogType = 0;
        SendToWrite(data, currentLog);
    }

    public void SendToWrite(string data, LogType fileType)
    {
        _pluginInstance.Call("writeToFile", "Logs.txt", data);
    }

    public void SendToReadFile()
    {
        string temp;
        temp = _pluginInstance.Call<string>("readFromFile", "Logs.txt");
        tempArray = temp.Split("\n");
        for (int i = 0; i < tempArray.Length; i++)
        {
            GameObject newText = Instantiate(textObject, textPanel.transform);
            newText.GetComponent<TextMeshProUGUI>().text = tempArray[i];
            createdLogs.Add(newText);
        }
    }

    public void ShowAlert()
    {
        for (int i = 0; i < createdLogs.Count; i++)
        {
            Destroy(createdLogs[i].gameObject);
            createdLogs.Remove(createdLogs[i]);
        }
        createdLogs.Clear();
        Debug.Log("Unity Alert Show");
        _pluginInstance.Call("ShowAlert");
    }
#endif
}