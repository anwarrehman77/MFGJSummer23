using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject spring, summer, fall, winter;

    private string level;

    void Start()
    {
        level = PlayerPrefs.GetString("LastAccessedLevel");
        
        switch (level)
        {
            case "Spring":
            spring.SetActive(true);
            break;
            case "Summer":
            summer.SetActive(true);
            break;
            case "Fall":
            fall.SetActive(true);
            break;
            case "Winter":
            winter.SetActive(true);
            break;
            case "":
            spring.SetActive(true);
            break;
        }
        
    }
}
