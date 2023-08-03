using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreScreenManager : MonoBehaviour
{
    [SerializeField]
    GameObject springBG, summerBG, fallBG, winterBG;

    [SerializeField]
    TMP_Text spring, pspring, summer, psummer, fall, pfall, winter, pwinter;

    private string level;

    void Start()
    {
        level = PlayerPrefs.GetString("LastAccessedLevel");

        switch (level)
        {
            case "Spring":
            springBG.SetActive(true);
            break;
            case "Summer":
            summerBG.SetActive(true);
            break;
            case "Fall":
            fallBG.SetActive(true);
            break;
            case "Winter":
            winterBG.SetActive(true);
            break;
            case "":
            springBG.SetActive(true);
            break;
        }

        spring.text += PlayerPrefs.GetInt("HighScoreSpring");
        pspring.text += PlayerPrefs.GetInt("LastScoreSpring");
        summer.text += PlayerPrefs.GetInt("HighScoreSummer");
        psummer.text += PlayerPrefs.GetInt("LastScoreSummer");
        fall.text += PlayerPrefs.GetInt("HighScoreFall");
        pfall.text += PlayerPrefs.GetInt("LastScoreFall");
        winter.text += PlayerPrefs.GetInt("HighScoreWinter");
        pwinter.text += PlayerPrefs.GetInt("LastScoreWinter");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
}
