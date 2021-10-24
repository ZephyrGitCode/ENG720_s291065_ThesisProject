using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.VR;

public class MainMenuLogic : MonoBehaviour
{
    // Start Menu
    public Canvas MainMenu;

    // Level Select
    public Canvas LevelSelectMenu;

    // Options
    public Canvas OptionsMenu;

    // Statistics
    public Canvas StatisticsMenu;

    // Stats_Stopping
    public Canvas Stats_StopMenu;

    // Stats_Collisions
    public Canvas Stats_Collisions;

    // Stats_Objectives
    public Canvas Stats_Objectives;

    private void Awake() {
        UnityEngine.XR.InputTracking.Recenter();
    }

    // Select level
    public void LevelSelectUi() {
        // Disable Other UI
        MainMenu.gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(false);

        // Enable level select
        LevelSelectMenu.gameObject.SetActive(true);
        Debug.Log("Set levelselect true"); 
    }

    // Return to main menu
    public void MainMenuUi() {
        // Enable Main UI
        MainMenu.gameObject.SetActive(true);

        // Disable others
        LevelSelectMenu.gameObject.SetActive(false);
        OptionsMenu.gameObject.SetActive(false);
        StatisticsMenu.gameObject.SetActive(false);
        Stats_StopMenu.gameObject.SetActive(false);
        Stats_Collisions.gameObject.SetActive(false);
        Stats_Objectives.gameObject.SetActive(false);
    }

    // Open Options menu
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

    // Open Statistics menu
    public void StatisticsMenuUi() {
        // Disable Main UI
        MainMenu.gameObject.SetActive(false);

        // Enable level select
        StatisticsMenu.gameObject.SetActive(true);
    }

    // Open Statistics menu
    public void Stats_StopMenuUi() {
        // Disable Main UI
        StatisticsMenu.gameObject.SetActive(false);

        // Enable level select
        Stats_StopMenu.gameObject.SetActive(true);
    }

    // Open Statistics menu
    public void Stats_CollisionsMenuUi() {
        // Disable Main UI
        StatisticsMenu.gameObject.SetActive(false);

        // Enable level select
        Stats_Collisions.gameObject.SetActive(true);
    }

    // Open Statistics menu
    public void Stats_ObjectivesMenuUi() {
        // Disable Main UI
        StatisticsMenu.gameObject.SetActive(false);

        // Enable level select
        Stats_Objectives.gameObject.SetActive(true);
    }

}
