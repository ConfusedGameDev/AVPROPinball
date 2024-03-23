using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LauncherStick : PinchInteractable
{
    public Transform stickTransform;
    public Vector3 startPos;
    public bool isLaunching;
    float delta;
    public Vector3 launchStarPos;
    public UnityEvent onFinishedLaunch;
    public float speed = 3f;
    public float force = 25f;
    public Ball ball;
    // Start is called before the first frame update
    void Start()
    {
        startPos = stickTransform.position;
    }

    public override void updateDelta(Vector3 currentDelta)
    {
        
        base.updateDelta(currentDelta);
        if (!isLaunching)
            stickTransform.position = startPos + stickTransform.forward * zDelta;
    }
    public void Activate()
    {
        Debug.Log("Activate Collider");
        GetComponent<Collider>().enabled = true;
    }
    public void onReleasePinch()
    {
        delta = 0;
        isLaunching = true;
        launchStarPos = stickTransform.position;
         
    }

    public void FixedUpdate()
    {
        if (isLaunching)
        {
            stickTransform.position = Vector3.Lerp(launchStarPos, startPos, delta);
            delta += Time.deltaTime*(speed*zDelta);
        }
        if (delta >= 1f)
        {
            delta = 0;
            isLaunching = false;
            onFinishedLaunch.Invoke();
            if(!ball)
            ball = FindObjectOfType<Ball>();
            if(ball)
            {
                ball.addImpulse(transform.forward * force);
            }

        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
