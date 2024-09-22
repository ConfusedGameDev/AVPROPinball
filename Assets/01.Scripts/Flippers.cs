using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Flippers : MonoBehaviour
{
    // Start is called before the first frame update

    public float restPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrenght = 10000f;
    public float flipperDamper = 150f;


    public KeyCode Key;
    public bool isDown { get; set; }

    HingeJoint hingeJoint;
    JointSpring flipperSpring;
    public bool useDebugKey = true;
    public AudioSource source;
    public AudioClip clip;
    bool isPressed;

    

    public GameObject ball;
    public float force = 250;
    public bool hasBall;
    public bool canAddImpulse = true;
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        hingeJoint.useSpring = true;
        hingeJoint.useLimits = true;        
        flipperSpring.spring = hitStrenght;
        flipperSpring.damper = flipperDamper;
    }

    public void DoInteraction()
    {
        flipperSpring.targetPosition = pressedPosition;
        if (source && !isPressed)
            source.PlayOneShot(clip);
        isPressed = true;
        
    }
    private void FixedUpdate()
    {
        if (isDown)
        {

            DoInteraction();
            if (canAddImpulse && hasBall)
            {
                ball.GetComponent<Rigidbody>().AddForce(transform.forward * force);
            }
            canAddImpulse = false;

        }
        else
        {
            canAddImpulse = true;
            isPressed = false;
            flipperSpring.targetPosition = restPosition;
        }
        hingeJoint.spring = flipperSpring;
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject == ball)
            hasBall = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (hasBall && collision.gameObject == ball)
            hasBall = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(useDebugKey)
        isDown = Input.GetKey(Key);       
        
    }
}
