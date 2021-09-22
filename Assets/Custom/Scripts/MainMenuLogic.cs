using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainMenuLogic : MonoBehaviour
{
    // Start Menu
    public Canvas MainMenu;

    // Level Select
    public Canvas LevelSelectMenu;

    // Options
    public Canvas OptionsMenu;

    // Select level
    public void LevelSelectUi() {
        // Disable Other UI
        MainMenu.gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(false);

        // Enable level select
        LevelSelectMenu.gameObject.SetActive(true);
        Debug.Log("Set levelselect true"); 
    }

    // Select level
    public void MainMenuUi() {
        // Enable Main UI
        MainMenu.gameObject.SetActive(true);

        // Disable others
        LevelSelectMenu.gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(false);
    }

    // Select level
    public void OptionsMenuUi() {
        // Disable Main UI
        MainMenu.gameObject.SetActive(false);

        // Enable level select
        OptionsMenu.gameObject.SetActive(true);
    }

    // Load selected level
    public void LoadLevel(string SceneName){
        SceneManager.LoadScene(SceneName);
    }
}
