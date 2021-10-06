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

    // Fail UI
    public Canvas FailUI;

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
        Vector3 endPosition = new Vector3(-10.14f,0.27f,19f);
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
        StopUI.gameObject.SetActive(false);
        StartCoroutine(GoToPass());
    }
    
    IEnumerator GoToPass()
    {
        // Slowly move car towards stop sign
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(-10.14f,0.27f,35.47f);
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


    public void MenuScene()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartScene()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }


    public void HitSomething()
    {
        //Make the level fail
        Debug.Log("Hit something!");
        Time.timeScale = 0; // stop time
        FailUI.gameObject.SetActive(true);
        // Show menu
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
        StopUI.gameObject.SetActive(false);
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
        StopUI.gameObject.SetActive(false);
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
        Vector3 endPosition = new Vector3(13.8f,0.549f,-0.51f);
        //Vector3 endPosition = StopRoad.transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.3f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }

        // Rotate car left
        Car.transform.Rotate(0f,-90f,0f);

        // Slowly move car to second stop
        startPosition = Car.transform.position;
        endPosition = new Vector3(-0.35f,0.549f,0.38f);
        //Vector3 endPosition = StopRoad.transform.position;
        travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.3f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }

        // Rotate car right
        Car.transform.Rotate(0f,90f,0f);

        // Once in position, Enable vehicle to move, enable stop button.
        stopPressed = false;
        stopButton.SetActive(true);
    }
    
    public void PressStop4()
    {
        if(vehicleStopped==false && enableStop==true){
            // stop vehicle
            stopPressed = !stopPressed;
            vehicleStopped = true;

            //Check vehicle center in snapzone
            StopInZone4();
        }
    }

    public void StopInZone4()
    {
        // get car's location x minus from stopzone location x
        float stopZonePos = stopZone.transform.position.z;
        float carPos = Car.transform.position.z;
        float perfectStop = 11.35f;
        
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
        StartCoroutine(GoToStop4_correct());
    }

    IEnumerator GoToStop4_correct()
    {
        // Slowly move car to first stop
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(-0.35f,0.549f,11.43f);
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
    
    public void PressGo4()
    {
        StopUI.gameObject.SetActive(false);
        StartCoroutine(GoToPass4());
    }
    
    IEnumerator GoToPass4()
    {
        // Slowly move car towards stop sign
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(-0.35f,0.549f,45f);
        //Vector3 endPosition = StopRoad.transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.3f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }
        WinUI.gameObject.SetActive(true);
    }

    /* Code for Riht of Way 5 Simple -----------------------------------------------------------------*/
    private GameObject[] interactors;

    public List<GameObject> vehicleOrder;

    public Canvas checkAnswersCanvas;

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
            checkAnswersCanvas.gameObject.SetActive(true);
            // reveal check answer button
        }
        
        //ColorBlock cb = RoadButton.colors;
        //cb.normalColor = new Color(0.2f, 0.7176471f, 0.2627451f, 0.3137255f);
        //RoadButton.colors = cb;

        // Get the outline of the parent
        //GetComponent<Outline>().enabled = true;
        
    }

    public void CheckAnswer()
    {
        float counter = 1;
        float correct = 0;
        float incorrect = 0;
        checkAnswersCanvas.gameObject.SetActive(false);

        foreach (GameObject Car in vehicleOrder)
        {
            Transform orderText = Car.transform.Find("Car_Interact/Panel/CarBtn/OrderText");
            TextMeshProUGUI orderTextUi = orderText.GetComponent<TextMeshProUGUI>();
            if(orderTextUi != null)
            {
                string checkstring = orderTextUi.text;
                Debug.Log("checkstring: "+checkstring[checkstring.Length-1].ToString());

                if(checkstring[checkstring.Length-1].ToString() == counter.ToString()) //counter.ToString()) Or 2
                {
                    // if last character matches counter, then correct
                    Debug.Log("Correct");
                    correct++;
                }else
                {
                    // if last character doesn't match, incorrect
                    Debug.Log("incorrect");
                    incorrect++;
                }
                counter++;
            }
            WinUI.gameObject.SetActive(true);
            Transform winTransform = WinUI.transform.Find("Panel/WinDesc");
            TextMeshProUGUI winText = winTransform.GetComponent<TextMeshProUGUI>();
            winText.text = "Score: "+correct+"/4 \n";
            if (correct == 4){
                // Player wins the level
                winText.text = winText.text+"Success!";
                //Complete!
            }else
            {
                // player lose UI, show score and restart/menu buttons.
                winText.text = winText.text+"Fail!";
            }
        }
    }

    /* code for 6 -----------------------------------------------------------------*/

    // Check Seat belt complete, move to checkpoint
    public void SeatBeltCheck6()
    {
        if (beltinout == true){
            // Disable UI canvas
            SBCanvas.enabled = false;
            
            // Enable vehicle to move, enable stop button.
            stopPressed = false;
            stopButton.SetActive(true);
        }
    }
    
    public void PressStop6()
    {
        if(vehicleStopped==false && enableStop==true){
            // stop vehicle
            stopPressed = !stopPressed;
            vehicleStopped = true;

            //Check vehicle center in snapzone, give rating
            StopInZone6();
        }
    }

    public void StopInZone6()
    {
        // get car's location x minus from stopzone location x
        float stopZonePos = stopZone.transform.position.z;
        float carPos = Car.transform.position.z;
        float perfectStop = 26.59f;
        
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
        StartCoroutine(GoToStop6());
    }

    IEnumerator GoToStop6()
    {
        // Slowly move car towards stop sign
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(11.23f,0.27f,26.59f);
        //Vector3 endPosition = StopRoad.transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.3f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }
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

    public void CarGo()
    {
        StartCoroutine(DelayedAnimation());
    }

    // The delay coroutine
     IEnumerator DelayedAnimation()
     {
        yield return new WaitForSeconds(4f);
        StopUI.gameObject.SetActive(true);
     }


    public void PressGo6()
    {
        StartCoroutine(GoToPass6());
    }

    IEnumerator GoToPass6()
    {
        StopUI.gameObject.SetActive(false);
        // rotate car slightly
        Car.transform.Rotate(0f,15f,0f);
        // Slowly move car towards stop sign
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(15.64f,0.27f,44.33f);
        //Vector3 endPosition = StopRoad.transform.position;
        float travelPercent = 0f;

        while(travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * 0.2f; // Speed
            Car.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }
        Car.transform.Rotate(0f,75f,0f);
        StartCoroutine(GoToPass6_2());
    }

    IEnumerator GoToPass6_2()
    {
        // Slowly move car towards stop sign
        Vector3 startPosition = Car.transform.position;
        Vector3 endPosition = new Vector3(24.77f,0.27f,45.42f);
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
}
