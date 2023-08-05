using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using TMPro;

public class ChooseCorrectAnswer : MonoBehaviour 
{
    [SerializeField]
    private GameObject signObj;
    private Sign sign;

    private TMP_Text text;
    private string[] answers;
    private string filePath;

    void Start()
    {
        sign = signObj.GetComponent<Sign>();

        filePath = @"Assets\Fall Mechanics\Math Answers.txt";
        answers = File.ReadAllLines(filePath);
        text = GetComponent<TMP_Text>();
        text.text = answers[sign.index];
    }
}
