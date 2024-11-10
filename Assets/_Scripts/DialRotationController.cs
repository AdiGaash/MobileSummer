using UnityEngine;

public class DialRotationController : MonoBehaviour
{
    // Sensitivity factor for controlling the rotation speed
    public float rotationSensitivity = 1.0f;

    private float initialRotationAngle;
    private bool isRotating;

    void Update()
    {
        // Check if there are exactly two touches on the screen
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Check if this is the beginning of the touch to initialize rotation
            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialRotationAngle = GetAngleBetweenTouches(touch1.position, touch2.position);
                isRotating = true;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                // Calculate current angle between the two touches
                float currentRotationAngle = GetAngleBetweenTouches(touch1.position, touch2.position);

                // Determine the change in angle
                float angleDelta = currentRotationAngle - initialRotationAngle;

                // Update the initial rotation angle for next frame
                initialRotationAngle = currentRotationAngle;

                // Apply rotation to the dial object
                transform.Rotate(0, 0, -angleDelta * rotationSensitivity);
            }
            else if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
            {
                // End rotation tracking when either finger lifts
                isRotating = false;
            }
        }
    }

    // Helper function to calculate the angle between two touch points
    private float GetAngleBetweenTouches(Vector2 touch1, Vector2 touch2)
    {
        Vector2 direction = touch2 - touch1;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}