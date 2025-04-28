
using UnityEngine;

public class SwipeDetector3 : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 50f; // Minimum swipe distance to be considered a swipe
    private float maxSwipeTime = 1.0f; // Maximum time in seconds to consider a swipe

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    DetectSwipe();
                    break;
            }
        }
    }

    void DetectSwipe()
    {
        // Calculate swipe distance
        float swipeDistance = (endTouchPosition - startTouchPosition).magnitude;

        // Calculate swipe duration
        float swipeDuration = Time.deltaTime;

        // Adjust swipe direction based on device orientation
        bool isLandscape = Screen.orientation  == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight;

        if (isLandscape)
        {
            // Landscape orientation
            if (endTouchPosition.x > startTouchPosition.x && swipeDistance > swipeThreshold && swipeDuration < maxSwipeTime)
            {
                Debug.Log("Swipe Right in Landscape");
            }
            else if (endTouchPosition.x < startTouchPosition.x && swipeDistance > swipeThreshold && swipeDuration < maxSwipeTime)
            {
                Debug.Log("Swipe Left in Landscape");
            }
        }
        else
        {
            // Portrait orientation (default or upside-down)
            if (endTouchPosition.y > startTouchPosition.y && swipeDistance > swipeThreshold && swipeDuration < maxSwipeTime)
            {
                Debug.Log("Swipe Up in Portrait");
            }
            else if (endTouchPosition.y < startTouchPosition.y && swipeDistance > swipeThreshold && swipeDuration < maxSwipeTime)
            {
                Debug.Log("Swipe Down in Portrait");
            }
        }
    }
}
