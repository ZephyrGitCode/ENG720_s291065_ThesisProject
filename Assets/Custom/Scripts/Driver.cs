using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Driver : MonoBehaviour
{
    [SerializeField] float xValue = 0f;
    [SerializeField] float yValue = 0f;
    [SerializeField] float moveSpeed = 10f;

    public bool DestinationNotPassed = true;
    //[SerializeField] float zValue = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(TravelForward());
    }

    // Update is called once per frame
    void Update()
    {
        //MovePlayer();
    }

    void MovePlayer()
    {
        float accelerate = 1;
        float zValue = (Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        transform.Translate(xValue,yValue,zValue);
    }

    IEnumerator TravelForward()
    {
        float accelerate = 1;
        while(DestinationNotPassed)
        {
            transform.position += (Vector3.forward * Time.deltaTime) * 5f;
            yield return new WaitForEndOfFrame();
        }
    }
}