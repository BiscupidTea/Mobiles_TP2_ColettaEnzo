using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;

public class PlayGamesManager : MonoBehaviour
{
    [SerializeField] private Text text;
    public void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();

            text.text = name;
        }
        else
        {
            text.text = "Error, Sign in Failed!";
        }
    }

}
