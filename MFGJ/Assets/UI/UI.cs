using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]
    GameObject HUD, PauseMenu, OptionsMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (HUD.activeSelf) LoadPauseMenu();
            else if (PauseMenu.activeSelf) LoadHUD();
            else if (OptionsMenu.activeSelf) LoadPauseMenu();
        }
    }

    public void LoadHUD()
    {
        Time.timeScale = 1f;
        HUD.SetActive(true);
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
    }

    public void LoadPauseMenu()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        HUD.SetActive(false);
        OptionsMenu.SetActive(false);
    }

    public void LoadOptionsMenu()
    {
        OptionsMenu.SetActive(true);
        PauseMenu.SetActive(false);
        HUD.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }
}
