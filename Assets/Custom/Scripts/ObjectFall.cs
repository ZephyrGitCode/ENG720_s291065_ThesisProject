using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFall : MonoBehaviour
{
    MeshRenderer renderer;
    Rigidbody Rigidbody;
    [SerializeField] float timeToWait=5f;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        Rigidbody = GetComponent<Rigidbody>();

        renderer.enabled=false;
        Rigidbody.useGravity=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeToWait)
        {
            renderer.enabled=true;
            Rigidbody.useGravity=true;
        }
    }
}
