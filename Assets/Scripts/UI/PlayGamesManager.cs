using System.Threading.Tasks;
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
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with Google Play games successful.");

                string name = PlayGamesPlatform.Instance.GetUserDisplayName();
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    
                });
                
                text.text = "Welcome " + name + "!";
            }
            else
            {
                Debug.Log("Login Unsuccessful");
                text.text = "Error, Sign in Failed!";
            }
        });
    }

}
