using UnityEngine;
using UnityEngine.InputSystem;

public class LightColorCycle : MonoBehaviour
{
    public Light light; // Assign this in the Unity Editor
    public InputActionReference colorChangeAction;

    private void OnEnable()
    {
        if (light == null)
            light = GetComponent<Light>();

        colorChangeAction.action.Enable();
        colorChangeAction.action.performed += ChangeColor;
    }

    private void ChangeColor(InputAction.CallbackContext context)
    {
        // Generate a random color
        Color randomColor = Random.ColorHSV();
        light.color = randomColor;
    }
}