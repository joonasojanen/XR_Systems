using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnifying : MonoBehaviour
{
    public CustomGrab customGrabScript;
    public GameObject magnifyingGlass;
    public Transform mainCameraTransform;
    public Transform lensCameraTransform;
    public Transform lensTransform;
    private Vector3 originalLocalPosition;
    private Quaternion originalLocalRotation;
    Vector3 currentEulerAngles;

    void Start()
    {
        originalLocalPosition = lensCameraTransform.localPosition;
        originalLocalRotation = lensCameraTransform.localRotation;
    }

    void Update()
    {
        if (customGrabScript.IsObjectGrabbed(magnifyingGlass))
        {
            currentEulerAngles = lensTransform.eulerAngles;

            // Align the child transform to face the direction the camera is facing
            lensCameraTransform.eulerAngles = new Vector3(mainCameraTransform.eulerAngles.x, mainCameraTransform.eulerAngles.y, currentEulerAngles.z);
        }
        else
        {
            lensCameraTransform.localPosition = originalLocalPosition;
            
            lensCameraTransform.localRotation = lensTransform.localRotation;       
        }
    }
}
