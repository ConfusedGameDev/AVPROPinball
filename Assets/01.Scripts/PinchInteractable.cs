using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PinchInteractable : MonoBehaviour
{
    public UnityEvent onPinch, onStopPinch;

    public float zDelta;
    public virtual void updateDelta(Vector3 currentDelta)
    {
        zDelta = Mathf.Clamp01(Mathf.Abs(currentDelta.z));

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
