using UnityEngine;
using UnityEngine.UI;

public class TwoFingerRotation : MonoBehaviour
{
    // Reference to the RawImage component
    public RawImage rawImage;

    // The angle of the rotation
    private float rotationAngle = 0f;

    // Store the initial angle between two touches
    private float initialTouchAngle;

    // Tracks if two fingers are being used
    private bool isRotating = false;

    void Update()
    {
        // Detect if there are two touches
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // If touches have just begun, initialize rotation
            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                isRotating = true;
                initialTouchAngle = GetAngleBetweenTouches(touch1, touch2);
            }

            // If fingers are moving, calculate the new rotation angle
            if (isRotating && (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved))
            {
                float currentTouchAngle = GetAngleBetweenTouches(touch1, touch2);
                float angleDelta = currentTouchAngle - initialTouchAngle;

                // Apply the rotation to the RawImage
                rotationAngle += angleDelta;
                rawImage.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

                // Update the initial touch angle
                initialTouchAngle = currentTouchAngle;
            }
        }

        // Reset the rotating state when touches end
        if (Input.touchCount < 2)
        {
            isRotating = false;
        }
    }

    // Calculate the angle between two touches
    private float GetAngleBetweenTouches(Touch touch1, Touch touch2)
    {
        Vector2 diff = touch2.position - touch1.position;
        return Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    }
}