    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PinchInteractable : MonoBehaviour
{
    public UnityEvent onPinch, onStopPinch;
    public bool isTouch;
    public bool canBeTouched;
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
    private void OnTriggerEnter(Collider other)
    {
        if(!isTouch) return;
        
        if(other.name.Contains("Hand"))
        {
            canBeTouched=false;
            onPinch?.Invoke();
        }

        

    }
    private void OnTriggerExit(Collider other)
    {
        if(!isTouch) return;

        if(other.name.Contains("Hand"))
        {
            canBeTouched=true;
            onStopPinch?.Invoke();
        }

        

    }
  
}
