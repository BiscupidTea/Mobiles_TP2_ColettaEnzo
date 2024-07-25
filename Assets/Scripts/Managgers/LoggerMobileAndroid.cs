using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class LoggerMobileAndroid : MonoBehaviour
{
    private const string packageName = "com.coletta.colettalogger";
    private const string className = packageName + ".Logger";
    public GameObject textMeshProPrefab;
    public Transform contentParent;
    private List<GameObject> logsList = new List<GameObject>();
#if UNITY_ANDROID
    private AndroidJavaClass PluginClass;
    private AndroidJavaClass unityPlayer;
    private AndroidJavaObject pluginInstance;
    private AndroidJavaObject unityActivity;

    private const string permission = "android.permission.WRITE_EXTERNAL_STORAGE";

#endif

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            PluginClass = new AndroidJavaClass(className);
            pluginInstance = DebugManager.Instance.PluginInstance;
        }
    }

    public void DeleteLogs()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            pluginInstance.Call("ShowAlert");
        }
    }

    public void ReadLogs()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            string logs = pluginInstance.Call<string>("readFile");
            string[] lines = logs.Split("\n");

            if (logsList.Capacity > 0)
            {
                foreach (GameObject VARIABLE in logsList)
                {
                    Destroy(VARIABLE);
                }
            }
            logsList.Clear();
            foreach (string line in lines)
            {
                GameObject textObject = Instantiate(textMeshProPrefab, contentParent);
                textObject.SetActive(true);
                TextMeshProUGUI textComponent = textObject.GetComponent<TextMeshProUGUI>();
                textComponent.text = line;
                logsList.Add(textObject);
            }

        }
    }

    public void TestWarning()
    {
        string text = "This is a Warning";
        Debug.LogWarning(text);
    }
    public void TestLog()
    {
        string text = "This is a Log";
        Debug.Log(text);
    }
    public void TestError()
    {
        string text = "This is a Error";
        Debug.LogError(text);
    }

}