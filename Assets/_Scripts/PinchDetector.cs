using UnityEngine;

public class PinchDetector : MonoBehaviour
{
    private float initialDistance;
    private bool isPinching = false;
    private float pinchThreshold = 0.01f; // Minimum change in distance to be considered a pinch

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                // Record the initial distance between the two touches
                initialDistance = Vector2.Distance(touch0.position, touch1.position);
                isPinching = true;
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                if (isPinching)
                {
                    // Calculate the current distance between the two touches
                    float currentDistance = Vector2.Distance(touch0.position, touch1.position);

                    // Determine the pinch direction
                    if (Mathf.Abs(currentDistance - initialDistance) > pinchThreshold)
                    {
                        if (currentDistance > initialDistance)
                        {
                            // Pinch out (zoom in)
                            Debug.Log("Pinch Out (Zoom In)");
                        }
                        else
                        {
                            // Pinch in (zoom out)
                            Debug.Log("Pinch In (Zoom Out)");
                        }
                        // Update the initial distance for the next comparison
                        initialDistance = currentDistance;
                    }
                }
            }
            else if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended || touch0.phase == TouchPhase.Canceled || touch1.phase == TouchPhase.Canceled)
            {
                // Reset pinching state when any touch ends or is canceled
                isPinching = false;
            }
        }
    }
}
