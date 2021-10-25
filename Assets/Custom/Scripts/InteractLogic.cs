using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InteractLogic : MonoBehaviour
{
    public Button SignButton;

    public Button RoadButton;

    public TextMeshProUGUI sceneScore;

    public bool stopSignBool = false;
    public bool road = false;
    public int score = 0;
    public int Score { get { return score; } }

    CustomSettings customSettings;

    private void Awake() {
        customSettings = FindObjectOfType<CustomSettings>();
    }

    // disabled until vehicle is moving, only do score while lerping

    private void Start() {
        // disabled by default
        //GetComponent<Outline>().enabled = true;
        // AddScore(1); -- debug
    }

    // Interact with Stop Sign
    public void StopSign() {
        // Add score, change colour
        ColorBlock cb = SignButton.colors;
        cb.normalColor = new Color(0.2f, 0.7176471f, 0.2627451f, 0.3137255f);
        SignButton.colors = cb;

        // Get the outline of the parent, on click enable
        //GetComponent<Outline>().enabled = true;

        // add score, 2 = stop sign
        AddScore(1);
    }

    public void RoadLine() {
        // Add score, change colour
        ColorBlock cb = RoadButton.colors;
        cb.normalColor = new Color(0.2f, 0.7176471f, 0.2627451f, 0.3137255f);
        RoadButton.colors = cb;

        // Get the outline of the parent
        //GetComponent<Outline>().enabled = true;

        // add score, 2 = road line
        AddScore(2);
    }

    private void AddScore(int interactInt)
    {
        score++;
        sceneScore.text = "Score: " + score + "/2";

        if(interactInt == 1 && stopSignBool == false)
        {
            // Ensure it can only be counted once, marks objective as done
            stopSignBool = true;
        }else if (interactInt == 2 && road == false)
        {
            // Ensure it can only be counted once, marks objective as done
            road = true;
        }

        //customSettings.SaveObjective(1); -- debug
        // main ui addscore
    }
}
