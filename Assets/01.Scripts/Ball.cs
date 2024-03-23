using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Ball : MonoBehaviour
{
    Vector3 startpos;
    Quaternion startRot;
    Rigidbody rb;
    public UnityEvent onReset;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        if (collision.transform.CompareTag("Reset"))
            ResetBall();
        else if (collision.transform.CompareTag("Launcher"))
        {
            Debug.Log("collided with launcher");
             var launcher= FindObjectOfType<LauncherStick>();
             if(launcher)
            {
                Debug.Log("try activate launcher");
                launcher.Activate();
            }
        }

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
        onReset.Invoke();
    }

    public void addImpulse(Vector3 dir)
    {
        if (rb)
        {
            rb.AddForce(dir);
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
