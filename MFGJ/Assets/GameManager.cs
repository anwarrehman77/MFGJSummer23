using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;

    [SerializeField]
    private TMP_Text scoreText, timeLeftText;
    [SerializeField]
    private float timeLimit;
    [SerializeField]
    private string level;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime;

        timeLeftText.text = Math.Round(timeLimit, 2).ToString();
        scoreText.text = score.ToString();
    }

    public void AddScore(int scoreUp)
    {
        score += scoreUp;
    }

    public void OnStageEnd()
    {
        score += Mathf.RoundToInt(timeLimit);
        PlayerPrefs.SetInt($"LastScore{level}", score);
        if (score > PlayerPrefs.GetInt($"HighScore{level}")) PlayerPrefs.SetInt($"HighScore{level}", score);
    }
}
