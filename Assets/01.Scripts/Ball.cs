using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 startpos;
    Quaternion startRot;
    Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        if (collision.transform.CompareTag("Reset"))
            ResetBall();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Reset"))
            ResetBall();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startpos = transform.position;
        startRot = transform.rotation;
    }

    public void ResetBall()
    {
        if(rb)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

        }
        transform.position = startpos;
        transform.rotation = startRot;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
