using UnityEngine;

public class AdjustClippingPlaneBasedOnAcceleration : MonoBehaviour
{
    // Reference to the main camera (or any other camera you want to adjust)
    public Camera cameraToAdjust;

    // Minimum and maximum values for the far clipping plane
    public float minClippingPlane = 50.0f;
    public float maxClippingPlane = 1000.0f;

    void Update()
    {
        // Get the current Z-axis acceleration
        float zAcceleration = Input.acceleration.z;

        // Map the Z-axis acceleration to the far clipping plane range
        // Z = -1 (face down) -> minClippingPlane
        // Z = +1 (face up) -> maxClippingPlane
        float newClippingPlane = Mathf.Lerp(maxClippingPlane, minClippingPlane, 1+ Mathf.Clamp(zAcceleration, -1.0f, 0.0f));

        // Apply the new far clipping plane to the camera
        cameraToAdjust.farClipPlane = newClippingPlane;

        // Debugging output to check values (optional)
        Debug.Log("Z-Acceleration: " + zAcceleration + ", Far Clipping Plane: " + newClippingPlane);
    }
}