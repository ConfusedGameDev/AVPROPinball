using UnityEngine;
using UnityEngine.Events;
public class HandControlSystem : MonoBehaviour
{
    public float pinchDistance = 0.1f;
    public float unPinchDistance = 0.35f;
    public float currentDistance;
    public enum state
    {
        idle,
        pinching
    }
    public state currentState;
    public Transform indexTip, thumbTip;
    public UnityEvent onPinch, onUnpinch;
    PinchInteractable currentPinchInteractable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        PinchInteractable pinchInteractable;
        if (collision.gameObject.TryGetComponent<PinchInteractable>(out pinchInteractable))
        {
            if(pinchInteractable.isTouch)
                pinchInteractable.onPinch.Invoke();
            else
            currentPinchInteractable = pinchInteractable;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (currentPinchInteractable && collision.gameObject == currentPinchInteractable.gameObject)
            currentPinchInteractable = null;
    }
    // Update is called once per frame
    void Update()
    {
        if(indexTip && thumbTip)
        {
            currentDistance = Vector3.Distance(indexTip.position, thumbTip.position);
            if (currentState == state.idle)
            {
                if(currentDistance < pinchDistance)
                {
                    currentState = state.pinching;
                    onPinch.Invoke();
                    if (currentPinchInteractable)
                        currentPinchInteractable.onPinch.Invoke();
                }
            }
            else if (currentState == state.pinching)
            {
                if (currentDistance > unPinchDistance)
                {
                    currentState = state.idle;
                    onUnpinch.Invoke();
                    if(currentPinchInteractable)
                        currentPinchInteractable.onStopPinch.Invoke();
                }
            }
        }
    }
}
