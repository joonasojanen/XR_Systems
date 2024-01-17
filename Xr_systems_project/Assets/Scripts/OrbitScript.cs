using UnityEngine;

public class OrbitScript : MonoBehaviour
{
    public GameObject earth; // Reference to the Earth GameObject
    public GameObject moon;  // Reference to the Moon GameObject

    // Rotation and orbit speeds are 20000 times faster than real life scenario
    private float earthRotationSpeed = (360f / 86400f) * 20000f; // Earth's rotation speed in degrees per second
    private float moonOrbitSpeed = (360f / 2360520f) * 20000f;   // Moon's orbit speed in degrees per second

    void Update()
    {
        // Rotate the Earth around its own axis
        earth.transform.Rotate(0, earthRotationSpeed * Time.deltaTime, 0);

        // Orbit the Moon around the Earth
        // Assuming that the Moon's orbit is in the Earth's equatorial plane for simplicity
        moon.transform.RotateAround(earth.transform.position, Vector3.up, moonOrbitSpeed * Time.deltaTime);
    }
}
