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
    bool isDown;

    HingeJoint hingeJoint;
    JointSpring flipperSpring;
    void Start()
    {
        hingeJoint = GetComponent<HingeJoint>();
        hingeJoint.useSpring = true;
        hingeJoint.useLimits = true;        
        flipperSpring.spring = hitStrenght;
        flipperSpring.damper = flipperDamper;
    }

    private void FixedUpdate()
    {
        if (isDown)
        {
            flipperSpring.targetPosition = pressedPosition;

        }
        else
        {
            flipperSpring.targetPosition = restPosition;
        }
        hingeJoint.spring = flipperSpring;
        
    }
    // Update is called once per frame
    void Update()
    {

        isDown = Input.GetKey(Key);       
        
    }
}
