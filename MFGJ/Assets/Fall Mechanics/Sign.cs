using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using TMPro;

public class Sign : MonoBehaviour
{
    private TMP_Text signText;
    private string[] problems;
    private int index;
    private string filePath;

    void Start()
    {
        filePath = @"Assets\Fall Mechanics\Math Problems.txt";
        problems = File.ReadAllLines(filePath);
        for (int i = 0; i < problems.Length; i++)
        {
            problems[i] += " x=?";
        }
        index = UnityEngine.Random.Range(0, problems.Length);
        signText = transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        signText.text = problems[index];
    }
}
