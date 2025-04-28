using UnityEngine;

public class MultiTouchHandler : MonoBehaviour
{
    void Update()
    {
        // Check how many touches are currently on the screen
        int touchCount = Input.touchCount;

        if(touchCount < 1)
            return;
        
        // Loop through all touches
        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Display information about each touch
            Debug.Log("Touch " + i + " - Position: " + touch.position + " Phase: " + touch.phase);

            // You can add additional touch handling logic here
            // For example, handle different touch phases
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Handle touch start
                    Debug.Log("Touch " + i + " started at position " + touch.position);
                    break;
                case TouchPhase.Moved:
                    // Handle touch moved
                    Debug.Log("Touch " + i + " moved to position " + touch.position);
                    break;
                case TouchPhase.Stationary:
                    // Handle touch stationary
                    Debug.Log("Touch " + i + " is stationary at position " + touch.position);
                    break;
                case TouchPhase.Ended:
                    // Handle touch end
                    Debug.Log("Touch " + i + " ended at position " + touch.position);
                    break;
                case TouchPhase.Canceled:
                    // Handle touch canceled
                    Debug.Log("Touch " + i + " was canceled at position " + touch.position);
                    break;
            }
        }
    }
}
