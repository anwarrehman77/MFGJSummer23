using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScores()
    {
        SceneManager.LoadScene("HighScoreScreen", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Debug.Log("Quit application");
        Application.Quit();
    }
}
