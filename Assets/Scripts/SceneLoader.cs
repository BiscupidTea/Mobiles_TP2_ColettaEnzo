using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadShop()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void LoadGame()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void LoadLoseScreen()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void Exit()
    {
        Application.Quit();

    }

    public void SetPause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
