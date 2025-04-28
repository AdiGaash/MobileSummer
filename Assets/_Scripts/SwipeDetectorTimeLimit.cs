
using UnityEngine; 
public class SwipeDetectorTimeLimit : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float startTime;
    private float endTime;
    private float swipeThreshold = 50f; // Minimum swipe distance to be considered a swipe
    private float timeLimit = 0.5f; // Maximum time in seconds for a swipe

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Record the starting touch position and time
                    startTouchPosition = touch.position;
                    startTime = Time.time;
                    break;

                case TouchPhase.Ended:
                    // Record the ending touch position and time
                    endTouchPosition = touch.position;
                    endTime = Time.time;
                    DetectSwipe();
                    break;
            }
        }
    }

    void DetectSwipe()
    {
        // Calculate the swipe distance in both directions
        float swipeDistanceX = endTouchPosition.x - startTouchPosition.x;
        float swipeDistanceY = endTouchPosition.y - startTouchPosition.y;

        // Calculate the swipe duration
        float swipeDuration = endTime - startTime;

        // Check if the swipe duration is within the time limit
        if (swipeDuration <= timeLimit)
        {
            // Check if the swipe distance is greater than the threshold
            if (Mathf.Abs(swipeDistanceX) > swipeThreshold || Mathf.Abs(swipeDistanceY) > swipeThreshold)
            {
                // Determine the direction of the swipe
                if (Mathf.Abs(swipeDistanceX) > Mathf.Abs(swipeDistanceY))
                {
                    if (swipeDistanceX > 0)
                    {
                        // Right swipe
                        Debug.Log("Swipe Right");
                    }
                    else
                    {
                        // Left swipe
                        Debug.Log("Swipe Left");
                    }
                }
                else
                {
                    if (swipeDistanceY > 0)
                    {
                        // Up swipe
                        Debug.Log("Swipe Up");
                    }
                    else
                    {
                        // Down swipe
                        Debug.Log("Swipe Down");
                    }
                }
            }
            else
            {
                Debug.Log("Swipe too short");
            }
        }
        else
        {
            Debug.Log("Swipe too slow");
        }
    }
}