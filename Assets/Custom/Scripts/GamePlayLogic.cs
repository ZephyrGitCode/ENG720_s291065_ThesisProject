using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GamePlayLogic : MonoBehaviour
{
    // Scene UI
    public TextMeshProUGUI SceneDesc;
    public Button ReadDone;

    // Seat belt UI
    public TextMeshProUGUI SBText;
    public Button checkbutton;
    public TextMeshProUGUI checktext;

    // Canvas
    public Transform sbclip;
    public Canvas SBCanvas;
    public Canvas GameUI;

    // Stop UI
    public Canvas StopUI;

    // Win UI
    public Canvas WinUI;

    // Move objects
    public GameObject StartRoad;
    public GameObject StopRoad;
    public GameObject PassRoad;
    public GameObject Car;

    private bool beltinout = false;

    // Check Seat belt complete, move to checkpoint
    public void SeatBeltCheck() {
        if (beltinout == true){
            // Disable UI canvas
            SBCanvas.enabled = false;
            
            // Slowly move car towards stop sign
            StartCoroutine(GoToStop());
        }
    }

    // Check Seat belt complete, move to checkpoint
    public void PressGo() {
        // Disable UI canvas
        StopUI.gameObject.SetActive(false);
        
        // Slowly move car towards Pass sign
        StartCoroutine(GoToPass());

        // End Scene
        Debug.Log("Well done");
    }

    IEnumerator GoToStop()
    {
        // Slowly move car towards stop sign
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(-0.35f,0.549f,18.59f);
        //Vector3 endPosition = StopRoad.transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.1f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }
        StopUI.gameObject.SetActive(true);
    }

     IEnumerator GoToPass()
    {
        // Slowly move car towards stop sign
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(-0.35f,0.549f,35.47f);
        //Vector3 endPosition = StopRoad.transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.2f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }
        WinUI.gameObject.SetActive(true);
    }

    // Move UI and open new one
    public void DescRead() {
        // Disable Scene UI then move it
        SceneDesc.gameObject.SetActive(false);
        ReadDone.gameObject.SetActive(false);

        // Enable new Game Scene UI
        GameUI.gameObject.SetActive(true);

        // Enable seat belt button
        checkbutton.gameObject.SetActive(true);
        SBText.gameObject.SetActive(true);
    }

    public void BeltIn() {
        // Belt is in, update text
        Debug.Log("Beltin");
        beltinout = true;
        ColorBlock cb = checkbutton.colors;
        cb.normalColor = Color.green;
        checkbutton.colors = cb;
        checktext.text = "Press me to begin";
        Debug.Log("Beltin - Done");
    }

    public void BeltOut() {
        Debug.Log("Beltout");
        // Belt is out, update text
        beltinout = false;
        ColorBlock cb = checkbutton.colors;
        cb.normalColor = Color.grey;
        checkbutton.colors = cb;
        checktext.text = "Seat belt please";
        Debug.Log("Beltout - Done");
    }

    public void MenuScene(){
        SceneManager.LoadScene("MainMenu");
    }
}
