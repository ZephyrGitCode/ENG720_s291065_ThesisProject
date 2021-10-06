using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{

    // Collision code
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag != "Hit" && gameObject.tag != "Environment")
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            gameObject.tag = "Hit";
        }
    }
}
