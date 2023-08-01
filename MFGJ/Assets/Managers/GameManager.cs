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
        PlayerPrefs.SetString("LastAccessedLevel", level);
    }

    // Update is called once per frame
    void Update()
    {
        timeLimit -= Time.deltaTime;

        timeLeftText.text = Math.Round(timeLimit, 2).ToString();
        scoreText.text = score.ToString();

        if (timeLimit <= 0)
        {
            Debug.Log("OutOfTime");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerHealth>().TakeDamage(int.MaxValue);
        }
    }

    public void OnStageEnd()
    {
        score += Mathf.RoundToInt(timeLimit);
        PlayerPrefs.SetInt($"LastScore{level}", score);
        if (score > PlayerPrefs.GetInt($"HighScore{level}")) PlayerPrefs.SetInt($"HighScore{level}", score);
    }

    public IEnumerator SetScoreText(int scoreUp, float duration)
    {
        int scoreTmp = score;
        int targetScore = score + scoreUp;
        float timeElapsed = 0;
        
        while (timeElapsed <= duration)
        {
            score = Mathf.RoundToInt(Mathf.Lerp(scoreTmp, targetScore, timeElapsed / duration));
            yield return null;
            timeElapsed += Time.deltaTime;
        }
    }
}
