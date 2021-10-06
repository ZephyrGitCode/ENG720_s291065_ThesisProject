using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopZone : MonoBehaviour
{
    private GamePlayLogic vehicleLogic;
    private void Awake() {
        vehicleLogic = FindObjectOfType<GamePlayLogic>();
    }

    private void OnTriggerEnter(Collider other) {
        // if player enters stopzone, enable vehicle stop
        // also turn on side camera?
        if(other.gameObject.tag == "Player")
        {
            vehicleLogic.enableStop = true;
            Debug.Log("In the area");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            vehicleLogic.stopPressed = true;
            Debug.Log("Left the area, fail");
        }
    }
}
