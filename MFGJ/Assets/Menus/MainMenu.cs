using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        string level = PlayerPrefs.GetString("LastAccessedLevel") != "" ? PlayerPrefs.GetString("LastAccessedLevel") : "Spring";

        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void LoadScores()
    {
        SceneManager.LoadScene("HighScoreScreen", LoadSceneMode.Single);
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("OptionsMenu", LoadSceneMode.Single);
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Debug.Log("Quit application");
        Application.Quit();
    }
}
