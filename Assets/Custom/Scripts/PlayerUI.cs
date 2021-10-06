using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    int hits = 0;

    private GamePlayLogic vehicleLogic;

    private void Awake() {
        vehicleLogic = FindObjectOfType<GamePlayLogic>();
    }

    private void OnCollisionEnter(Collision other) {
        if (gameObject.tag == "Player" && other.gameObject.tag == "Hitable")
        {
            hits++;
            Debug.Log("You Have Hit Something: " + hits+ " "+other.gameObject.name);
            vehicleLogic.HitSomething();
        }
    }
}
