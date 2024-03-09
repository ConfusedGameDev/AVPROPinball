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
    public Vector3 interactionPosition;
    PinchInteractable currentPinchObject;
    void OnEnable()
    {
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
            interactionPosition = primaryTouchData.interactionPosition;

            debugObject.transform.position = interactionPosition;
            
                
                if (objectBeingInteractedWith.TryGetComponent<PinchInteractable>(out currentPinchObject))
                {
                    currentPinchObject.onPinch.Invoke();
                }
                   
            

            
        }
        else
        {
             if(currentPinchObject)
            {
                currentPinchObject.onStopPinch.Invoke();
                currentPinchObject = null;
            }

        }

    }
}