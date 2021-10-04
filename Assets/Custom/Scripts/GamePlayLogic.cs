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
    public Canvas SBCanvas;
    public Canvas GameUI;

    // Stop UI
    public Canvas StopUI;

    // Win UI
    public Canvas WinUI;

    // Move objects
    public GameObject Car;
    public GameObject stopZone;
    private GameObject stopButton;
    public bool stopPressed = true;

    private bool beltinout = false;

    public bool moving = false;

    public bool vehicleStopped = false;

    public bool enableStop = false;

    private void Awake() {
        stopButton = GameObject.Find("BButton.R");
        stopButton.SetActive(false);
        stopZone = GameObject.Find("StopZone");
        stopPressed = true;
    }

    private void Update() {
        // move the driver forward until they stop
        if (stopZone!=null)
        {
            // Only do this on levels with a stopzone (3,4, 5?)
            if(stopPressed==false){
                moving=true;
                Car.transform.position += (Vector3.forward * Time.deltaTime) * 5f;
            }
        }
    }

    // Check Seat belt complete, move to checkpoint
    public void SeatBeltCheck()
    {
        if (beltinout == true){
            // Disable UI canvas
            SBCanvas.enabled = false;
            
            // Slowly move car towards the next player path object
            StartCoroutine(GoToStop());
        }
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
            travelPercent += Time.deltaTime * 0.3f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }
        StopUI.gameObject.SetActive(true);
    }

    public void PressGo()
    {
        StartCoroutine(GoToPass());
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
    public void DescRead()
    {
        // Disable Scene UI then move it
        SceneDesc.gameObject.SetActive(false);
        ReadDone.gameObject.SetActive(false);

        // Enable new Game Scene UI
        GameUI.gameObject.SetActive(true);

        // Enable seat belt button
        checkbutton.gameObject.SetActive(true);
        SBText.gameObject.SetActive(true);
    }

    public void BeltIn()
    {
        // Belt is in, update text
        Debug.Log("Beltin");
        beltinout = true;
        ColorBlock cb = checkbutton.colors;
        cb.normalColor = Color.green;
        checkbutton.colors = cb;
        checktext.text = "Press me to begin";
        Debug.Log("Beltin - Done");
    }

    public void BeltOut()
    {
        Debug.Log("Beltout");
        // Belt is out, update text
        beltinout = false;
        ColorBlock cb = checkbutton.colors;
        cb.normalColor = Color.grey;
        checkbutton.colors = cb;
        checktext.text = "Seat belt please";
        Debug.Log("Beltout - Done");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    /* Code for 2 -----------------------------------------------------------------*/
    
    public void SeatBeltCheck2()
    {
        if (beltinout == true){
            // Disable UI canvas
            SBCanvas.enabled = false;
            
            // Slowly move car towards the next player path object
            StartCoroutine(GoToStop2());
        }
    }

    IEnumerator GoToStop2()
    {
        // Slowly move car towards stop sign
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(-10.52f,0.549f,18.59f);
        //Vector3 endPosition = StopRoad.transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.2f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }
        StopUI.gameObject.SetActive(true);
    }

    public void PressGo2()
    {
        StartCoroutine(GoToPass2());
    }

    IEnumerator GoToPass2()
    {
        // Slowly move car towards stop sign
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(-10.52f,0.549f,35.47f);
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

    /* code for 3 -----------------------------------------------------------------*/

    // Check Seat belt complete, move to checkpoint
    public void SeatBeltCheck3()
    {
        if (beltinout == true){
            // Disable UI canvas
            SBCanvas.enabled = false;
            
            // Enable vehicle to move, enable stop button.
            stopPressed = false;
            stopButton.SetActive(true);
        }
    }
        public void PressStop()
    {
        if(vehicleStopped==false && enableStop==true){
            // stop vehicle
            stopPressed = !stopPressed;
            vehicleStopped = true;

            //Check vehicle center in snapzone, give rating
            StopInZone3();
        }
    }

    public void StopInZone3()
    {
        // get car's location x minus from stopzone location x
        float stopZonePos = stopZone.transform.position.z;
        float carPos = Car.transform.position.z;
        float perfectStop = 18.59f;
        
        // 18.59f is perfect stop
        float accuracy = (carPos - perfectStop);
        Debug.Log("Car Position: "+carPos+" | Perfect stop "+perfectStop+" | Accuracy: "+accuracy);

        /*
        switch (accuracy)
        {
            case accuracy<-5:
                // Less than value is very bad
                break;
            case accuracy<-2:
                // Less than value is very bad
                break;
            default:
                break;
        }
        */

        // Build a range, if in range good, if out of range bad.
        // if good: re-center car, wait for cars and press button.
        // if fail, restart level

        //StopUI.gameObject.SetActive(true);
        StartCoroutine(GoToStop3());
    }

    IEnumerator GoToStop3()
    {
        // Slowly move car towards stop sign
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(-0.35f,0.549f,18.59f);
        //Vector3 endPosition = StopRoad.transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.3f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }
        StopUI.gameObject.SetActive(true);
    }

    public void PressGo3()
    {
        StartCoroutine(GoToPass3());
    }

    IEnumerator GoToPass3()
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

    /* Code for StopSign 4 Advanced -----------------------------------------------------------------*/
    // Check Seat belt complete, move to checkpoint
    public void SeatBeltCheck4()
    {
        if (beltinout == true){
            // Disable UI canvas
            SBCanvas.enabled = false;
            
            // Move vehicle to winding position
            StartCoroutine(GoToStop4());
        }
    }
    IEnumerator GoToStop4()
    {
        // Slowly move car to first stop
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(-0.35f,0.549f,18.59f);
        //Vector3 endPosition = StopRoad.transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.3f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }

        // Rotate car left

        // Slowly move car to second stop
        startPosition = Car.transform.position;
        endPosition = new Vector3(-0.35f,0.549f,18.59f);
        //Vector3 endPosition = StopRoad.transform.position;
        travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.3f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }

        // Rotate car right

        // Slowly move car to third stop
        startPosition = Car.transform.position;
        endPosition = new Vector3(-0.35f,0.549f,18.59f);
        //Vector3 endPosition = StopRoad.transform.position;
        travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.3f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }


        // Once in position, Enable vehicle to move, enable stop button.
        stopPressed = false;
        stopButton.SetActive(true);
        StartCoroutine(GoToStop4_Crossing());
    }

    IEnumerator GoToStop4_Crossing()
    {
        // Slowly move car towards stop sign
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(-0.35f,0.549f,18.59f);
        //Vector3 endPosition = StopRoad.transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.3f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }
        StopUI.gameObject.SetActive(true);
    }

    public void StopInZone4()
    {
        // get car's location x minus from stopzone location x
        float stopZonePos = stopZone.transform.position.z;
        float carPos = Car.transform.position.z;
        float perfectStop = 14.59f;
        
        // 18.59f is perfect stop
        float accuracy = (carPos - perfectStop);
        Debug.Log("Car Position: "+carPos+" | Perfect stop "+perfectStop+" | Accuracy: "+accuracy);

        /*
        switch (accuracy)
        {
            case accuracy<-5:
                // Less than value is very bad
                break;
            case accuracy<-2:
                // Less than value is very bad
                break;
            default:
                break;
        }
        */

        // Build a range, if in range good, if out of range bad.
        // if good: re-center car, wait for cars and press button.
        // if fail, restart level

        StopUI.gameObject.SetActive(true);
    }
    
    public void PressGo4()
    {
        StartCoroutine(GoToPass4());
    }
    
    IEnumerator GoToPass4()
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

    /* Code for Riht of Way 5 Simple -----------------------------------------------------------------*/
    public GameObject[] interactors;

    public List<GameObject> vehicleOrder;

    private float carSelect = 1f;

    // Check Seat belt complete, move to checkpoint
    public void SeatBeltCheck5()
    {
        if (beltinout == true){
            // Disable UI canvas
            SBCanvas.enabled = false;

            EnableInteractors(); // Enables the interactor items
        }
    }
    
    void EnableInteractors()
    {
        interactors = GameObject.FindGameObjectsWithTag("Interactors");

        foreach(GameObject interactor in interactors)
        {
            if(interactor != null)
            {
                interactor.GetComponent<Button>().enabled = true;
                interactor.GetComponent<Image>().enabled = true;
            }
        }
    }

    public void VehicleSelect(GameObject Car) {
        Debug.Log("Hit Car: "+Car);

        

        Transform orderText = Car.transform.Find("Car_Interact/Panel/CarBtn/OrderText");
        TextMeshProUGUI orderTextUi = orderText.GetComponent<TextMeshProUGUI>();
        orderTextUi.text = "Order: "+carSelect;
        carSelect++;
        // if carSelect over the length of cars in list
        if (carSelect==5f)
        {
            carSelect = 1f;
            // reveal check answer button?
        }
        
        

        //ColorBlock cb = RoadButton.colors;
        //cb.normalColor = new Color(0.2f, 0.7176471f, 0.2627451f, 0.3137255f);
        //RoadButton.colors = cb;

        // Get the outline of the parent
        //GetComponent<Outline>().enabled = true;
        
    }

    public void CheckAnswer()
    {

        foreach (GameObject Car in vehicleOrder)
        {
            Transform orderText = Car.transform.Find("Car_Interact/Panel/CarBtn/OrderText");
            TextMeshProUGUI orderTextUi = orderText.GetComponent<TextMeshProUGUI>();
            if(orderTextUi != null)
            {

            }
        }

        // foreach vhicle in predefined list,
        // muscle value = 1 correct, else incorrect
        // if matches
        // else
    }
    
}
