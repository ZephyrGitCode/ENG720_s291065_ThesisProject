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
            string stringObject = other.gameObject.name;
            Debug.Log("You Have Hit Something: " + hits+ " "+ stringObject);
            // other is car or pedestrian?
            if(stringObject.Contains("Character"))
            {
                // 1 = Pedestrian
                int objectHit = 1;
                vehicleLogic.HitSomething(objectHit);
            }else
            {
                // 2 = Car
                int objectHit = 2;
                vehicleLogic.HitSomething(objectHit);
            }
        }
    }
}
