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

    public int score = 0;
    public int Score { get { return score; } }

    // disabled until vehicle is moving, only do score while lerping

    private void Start() {
        // disabled by default
        //GetComponent<Outline>().enabled = true;
    }

    // Interact with Stop Sign
    public void StopSign() {
        // Add score, change colour
        ColorBlock cb = SignButton.colors;
        cb.normalColor = new Color(0.2f, 0.7176471f, 0.2627451f, 0.3137255f);
        SignButton.colors = cb;

        // Get the outline of the parent, on click enable
        //GetComponent<Outline>().enabled = true;

        // add score
        AddScore();
    }

    public void RoadLine() {
        // Add score, change colour
        ColorBlock cb = RoadButton.colors;
        cb.normalColor = new Color(0.2f, 0.7176471f, 0.2627451f, 0.3137255f);
        RoadButton.colors = cb;

        // Get the outline of the parent
        //GetComponent<Outline>().enabled = true;

        // add score
    }

    public void AddScore()
    {
        score++;
        sceneScore.text = "Score: " + score + "/2"; 
        // main ui addscore
    }
}
