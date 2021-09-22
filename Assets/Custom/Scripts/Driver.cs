using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Valve.VR.Extras
    {
    public class Driver : MonoBehaviour
    {
        [SerializeField] float xValue = 0f;
        [SerializeField] float yValue = 0f;
        [SerializeField] float moveSpeed = 10f;
        //[SerializeField] float zValue = 0.01f;

        // Start is called before the first frame update
        void Start()
        {
            PrintInstructions();
        }

        // Update is called once per frame
        void Update()
        {
            MovePlayer();
        }

        void PrintInstructions()
        {
            Debug.Log("Welcome to the game");
            Debug.Log("Move forward and backwards with up and down arrows");
        }

        void MovePlayer()
        {
            float accelerate = 1;
            float zValue = (Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
            transform.Translate(xValue,yValue,zValue);
        }
    }
}