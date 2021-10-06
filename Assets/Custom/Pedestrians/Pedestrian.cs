using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);
    }

    public void ResetPosition()
    {
        gameObject.transform.position = new Vector3(8.371751f,0.4112847f,10.09316f);
    }
}
