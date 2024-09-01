using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : PinchInteractable
{
    public bool isActive = true;
    public Transform callToAction;
    public Vector3 scaleMaxSize = new Vector3(1, 1, 1); // Maximum scale size
    public float speed = 1.0f; // Speed of the ping-pong effect

    private Vector3 initialScale;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        setup();

    }
    public void setup()
    {
        initialScale = transform.localScale;
        callToAction.gameObject.SetActive(true);
    }
    public void onClicked()
    {
        callToAction.gameObject.SetActive(false);
        isActive = false;
        callToAction.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive)
        {
            time += Time.deltaTime * speed;

            // Calculate the ping-pong effect using Mathf.PingPong
            float pingPongValue = Mathf.PingPong(time, 1.0f);

            // Lerp between the initial scale (zero) and the max size
            callToAction.localScale = Vector3.Lerp(Vector3.zero, scaleMaxSize, pingPongValue);
        }
         
    }
}
