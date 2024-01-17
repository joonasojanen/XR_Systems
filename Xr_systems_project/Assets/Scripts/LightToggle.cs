using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightToggle : MonoBehaviour
{
    public Light light; // Assign this in the Unity Editor

    // Input Action References
    public InputActionReference toggleAction;
    public InputActionReference colorChangeAction;

    public Color defaultColor = Color.white; // Set your default color here

    private void OnEnable()
    {
        if (light == null)
            light = GetComponent<Light>();

        light.color = defaultColor; // Set light to default color when enabled

        // Enable actions and subscribe to their events
        toggleAction.action.Enable();
        colorChangeAction.action.Enable();
        toggleAction.action.performed += ToggleLight;
        colorChangeAction.action.performed += ChangeColor;
    }

    private void OnDisable()
    {
        // Unsubscribe from actions and disable them
        toggleAction.action.performed -= ToggleLight;
        colorChangeAction.action.performed -= ChangeColor;
        toggleAction.action.Disable();
        colorChangeAction.action.Disable();
    }

    private void ToggleLight(InputAction.CallbackContext context)
    {
        light.enabled = !light.enabled;
        if (!light.enabled)
        {
            light.color = defaultColor;
        }
    }

    private void ChangeColor(InputAction.CallbackContext context)
    {
        // Generate a random color
        if (light.enabled)
        {
            Color randomColor = Random.ColorHSV();
            light.color = randomColor;
        }
    }
}
