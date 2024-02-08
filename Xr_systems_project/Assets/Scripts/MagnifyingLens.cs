using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingLens : MonoBehaviour
{
    public CustomGrab customGrabScript;
    public GameObject magnifyingGlass;
    public Transform mainCameraTransform;
    public Transform childTransform;
    public Transform lensTransform;
    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;
    public Camera lensCamera;
    Vector3 currentEulerAngles;

    void Start()
    {
        originalLocalPosition = childTransform.localPosition;
        originalLocalRotation = childTransform.localRotation;
    }

    void Update()
    {
        if (customGrabScript.IsObjectGrabbed(magnifyingGlass))
        {
            currentEulerAngles = lensTransform.eulerAngles;
            lensTransform.eulerAngles = new Vector3(currentEulerAngles.x, currentEulerAngles.y, mainCameraTransform.eulerAngles.z);

            // Align the child transform to face the direction the camera is facing
            childTransform.eulerAngles = new Vector3(mainCameraTransform.eulerAngles.x, mainCameraTransform.eulerAngles.y, currentEulerAngles.z);
        }
        else
        {
            childTransform.localPosition = originalLocalPosition;
            
            childTransform.localRotation = lensTransform.localRotation;       
        }
    }
}
