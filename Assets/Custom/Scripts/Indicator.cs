using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 20f)] float indicatorDelay = 1f;
    private bool indicatorOn;

    // Start is called before the first frame update
    void Start()
    {
        indicatorOn = true;
        // call coroutine, flash lights.
        StartCoroutine(FlashIndicator());
    }

    IEnumerator FlashIndicator()
    {
        // Flash lights on and off
        while(true)
        {
            ChangeLights();
            indicatorOn = !indicatorOn;
            yield return new WaitForSeconds(indicatorDelay);
        }
    }
    void ChangeLights()
    {
        GetComponent<Renderer>().enabled = !indicatorOn;
        GetComponent<Light>().enabled = !indicatorOn;
        return;
    }
}
