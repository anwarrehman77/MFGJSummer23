using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScreenManager : MonoBehaviour
{
    [SerializeField]
    GameObject sampleBG;

    [SerializeField]
    TMP_Text sample, psample, spring, pspring, summer, psummer, fall, pfall, winter, pwinter;

    private string level;

    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetString("LastAccessedLevel");

        if (level == "Sample") sampleBG.SetActive(true);
        else Debug.Log("No previous level");

        sample.text += PlayerPrefs.GetInt("HighScoreSample").ToString();
        psample.text += PlayerPrefs.GetInt("LastScoreSample").ToString();
    }
}
