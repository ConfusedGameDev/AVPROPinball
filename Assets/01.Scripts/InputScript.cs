using Unity.PolySpatial.InputDevices;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.LowLevel;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputScript : MonoBehaviour
{
    public GameObject debugObject;
    public GameObject objectBeingInteractedWith;
     
    public SpatialPointerKind interactionKind;
    public Vector3 interactionPositionStart, currentInteractionPosition , interactionPositionEnd, interactionPositionDelta;
    PinchInteractable currentPinchObject;
    bool isPinching;
    void OnEnable()
    {
        isPinching = false;
        EnhancedTouchSupport.Enable();
    }

    void Update()
    {
        var activeTouches = Touch.activeTouches;
        debugObject.SetActive(activeTouches.Count > 0);
        // You can determine the number of active inputs by checking the count of activeTouches
        if (activeTouches.Count > 0)
        {
            // For getting access to PolySpatial (visionOS) specific data you can pass an active touch into the EnhancedSpatialPointerSupport()
            SpatialPointerState primaryTouchData = EnhancedSpatialPointerSupport.GetPointerState(activeTouches[0]);

            interactionKind = primaryTouchData.Kind;
            objectBeingInteractedWith = primaryTouchData.targetObject;

            
            debugObject.transform.position = interactionPositionStart;
            if (!isPinching)
            {
                isPinching = true;
                interactionPositionStart = primaryTouchData.interactionPosition;
            }
            currentInteractionPosition = primaryTouchData.interactionPosition;
            
                if (objectBeingInteractedWith.TryGetComponent<PinchInteractable>(out currentPinchObject))
                {
                    currentPinchObject.onPinch.Invoke();
                    currentPinchObject.updateDelta(interactionPositionDelta);
                
                    
                }

            interactionPositionDelta = primaryTouchData.interactionPosition-interactionPositionStart;


        }   
        else
        {
            isPinching = false;
            if (currentPinchObject)
            {
                
                interactionPositionEnd = currentInteractionPosition;    
                currentPinchObject.onStopPinch.Invoke();
                currentPinchObject = null;
            }

        }

    }
}