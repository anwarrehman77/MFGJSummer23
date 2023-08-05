using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;

public class GenerateRandomAnswer : MonoBehaviour 
{
    private TMP_Text text;
    private string[] answers;
    private string filePath;

    void Start()
    {
        filePath = @"Assets\Fall Mechanics\Fake Math Answers.txt";
        answers = File.ReadAllLines(filePath);
        text = GetComponent<TMP_Text>();
        int index = UnityEngine.Random.Range(0, answers.Length);
        text.text = answers[index];
    }
}
