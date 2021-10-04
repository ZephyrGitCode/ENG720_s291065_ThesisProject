using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    int hits = 0;

    private void OnCollisionEnter(Collision other) {
        if (gameObject.tag == "Player" && other.gameObject.tag != "Hit" && other.gameObject.tag != "Environment")
        {
            hits++;
            //Debug.Log("You Have Hit Something: " + hits+ " "+other.gameObject.name);
        }
    }
}
