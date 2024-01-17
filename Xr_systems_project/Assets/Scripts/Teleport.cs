using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    public GameObject player;
    public InputActionReference teleportAction;
    private Vector3 newLocation = new Vector3(-36f, 0f, 35f);
    private Quaternion newRotation = Quaternion.Euler(0, 135, 0);
    private Vector3 originalLocation =  new Vector3(0f, 0f, -3f) ;
    private Quaternion originalRotation = Quaternion.Euler(0, 180, 0);

    private void OnEnable()
    {
        teleportAction.action.Enable();
        teleportAction.action.performed += NewLocation;
    }

    private void NewLocation(InputAction.CallbackContext context)
    {
        if (player.transform.position == newLocation)
        {
            player.transform.position = originalLocation;
            player.transform.rotation = originalRotation;
        }
        else
        {
            player.transform.position = newLocation;
            player.transform.rotation = newRotation;
        }
    }
}
