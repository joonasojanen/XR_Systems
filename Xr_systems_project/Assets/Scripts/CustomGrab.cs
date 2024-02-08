using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;
    Vector3 lastPosition;
    Quaternion lastRotation;
    public InputActionReference toggleRotationAction;
    bool doubleRotationEnabled = false;

    private void Start()
    {
        action.action.Enable();
        toggleRotationAction.action.Enable();
        toggleRotationAction.action.performed += _ => doubleRotationEnabled = !doubleRotationEnabled;

        // Find the other hand
        foreach(CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject && nearObjects.Count > 0)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            { 
                // Change these to add the delta position and rotation instead
                // Save the position and rotation at the end of Update function, so you can compare previous pos/rot to current here
                Vector3 positionDelta = transform.position - lastPosition;
                Quaternion rotationDelta = transform.rotation * Quaternion.Inverse(lastRotation);

                if (doubleRotationEnabled)
                    rotationDelta = Quaternion.LerpUnclamped(Quaternion.identity,rotationDelta, 2f);

                Vector3 objectToController = rotationDelta * (grabbedObject.position - transform.position);
                //objectToController = rotationDelta * objectToController;

                if (otherHand != null && otherHand.grabbing && otherHand.grabbedObject == grabbedObject)
                {
                    // Combine translations and rotations for two-handed grabbing
                    Vector3 otherPositionDelta = otherHand.transform.position - otherHand.lastPosition;
                    Quaternion otherRotationDelta = otherHand.transform.rotation * Quaternion.Inverse(otherHand.lastRotation);
                    
                    if (doubleRotationEnabled)
                        otherRotationDelta = Quaternion.LerpUnclamped(Quaternion.identity, otherRotationDelta, 2.0f);
                    
                    grabbedObject.position += (positionDelta + otherPositionDelta + objectToController) - (grabbedObject.position - transform.position);
                    grabbedObject.rotation = rotationDelta * otherRotationDelta * grabbedObject.rotation;
                }
                else
                {
                    // Single-handed grabbing
                    grabbedObject.position += positionDelta + objectToController - (grabbedObject.position - transform.position);
                    grabbedObject.rotation = rotationDelta * grabbedObject.rotation;
                }
            }
        }
        // If let go of button, release object
        else if (grabbedObject)
            grabbedObject = null;

        // Should save the current position and rotation here
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="grabbable")
            nearObjects.Remove(t);
    }

    public bool IsObjectGrabbed(GameObject objectToCheck)
    {
        return grabbedObject != null && grabbedObject.gameObject == objectToCheck;
    }
    
}