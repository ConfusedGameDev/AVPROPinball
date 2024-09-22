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
        {
            ScoreManager.Instance.onReset();    
            ResetBall();
        }
            
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
        else if(collision.transform.CompareTag("Obstacle"))
        {
            InteractableObstacle obstacle;

            if(collision.transform.TryGetComponent<InteractableObstacle>(out obstacle))
            {
                obstacle.onActivate();
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Reset"))
        {
            ScoreManager.Instance.onReset();
            ResetBall();
            
        }
        else if (other.transform.CompareTag("Obstacle"))
        {
            InteractableObstacle obstacle;

            if (other.transform.TryGetComponent<InteractableObstacle>(out obstacle))
            {
                obstacle.onActivate();
            }
        }

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
            rb.linearVelocity = Vector3.zero;
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
