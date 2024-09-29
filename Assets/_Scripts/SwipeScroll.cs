using UnityEngine;
using UnityEngine.UI;

public class SwipeScroll : MonoBehaviour
{
    public ScrollRect horizontalScrollRect;  // Reference to the horizontal ScrollRect
    public ScrollRect verticalScrollRect;    // Reference to the vertical ScrollRect
    public float swipeAmount = 0.2f;         // Fixed amount to move on each swipe (0 to 1, normalized)
    public float minSwipeDistance = 20f;     // Minimum distance for a swipe to be registered

    private Vector2 startTouchPosition;      // Position where the touch began
    private Vector2 endTouchPosition;        // Position where the touch ended

    void Update()
    {
        HandleSwipe();
    }

    // Method to detect swipe and adjust scroll position
    void HandleSwipe()
    {
        if (Input.touchCount == 1)  // Ensure there is one touch on the screen
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Record the position when the touch begins
                startTouchPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                // Record the position when the touch ends
                endTouchPosition = touch.position;

                // Calculate the swipe distance on both axes
                float swipeDistanceX = endTouchPosition.x - startTouchPosition.x;
                float swipeDistanceY = endTouchPosition.y - startTouchPosition.y;

                // Check if it's a horizontal or vertical swipe
                if (Mathf.Abs(swipeDistanceX) > Mathf.Abs(swipeDistanceY)) // Horizontal swipe
                {
                    if (Mathf.Abs(swipeDistanceX) > minSwipeDistance)
                    {
                        // Determine swipe direction for horizontal scroll
                        if (swipeDistanceX > 0)
                        {
                            // Swiped right
                            ScrollHorizontal(-swipeAmount);
                        }
                        else
                        {
                            // Swiped left
                            ScrollHorizontal(swipeAmount);
                        }
                    }
                }
                else // Vertical swipe
                {
                    if (Mathf.Abs(swipeDistanceY) > minSwipeDistance)
                    {
                        // Determine swipe direction for vertical scroll
                        if (swipeDistanceY > 0)
                        {
                            // Swiped up
                            ScrollVertical(-swipeAmount);
                        }
                        else
                        {
                            // Swiped down
                            ScrollVertical(swipeAmount);
                        }
                    }
                }
            }
        }
    }

    // Method to adjust the horizontal scroll position
    void ScrollHorizontal(float amount)
    {
        // Move the horizontal scroll by the fixed amount, clamped between 0 and 1
        float newScrollPosition = Mathf.Clamp(horizontalScrollRect.horizontalNormalizedPosition + amount, 0.2f, 1f);
        horizontalScrollRect.horizontalNormalizedPosition = newScrollPosition;
        Debug.Log("ScrollHorizontal: " + newScrollPosition);
    }

    // Method to adjust the vertical scroll position
    void ScrollVertical(float amount)
    {
        // Move the vertical scroll by the fixed amount, clamped between 0 and 1
        float newScrollPosition = Mathf.Clamp(verticalScrollRect.verticalNormalizedPosition + amount, 0.2f, 1f);
        verticalScrollRect.verticalNormalizedPosition = newScrollPosition;
        Debug.Log("ScrollVertical: " + newScrollPosition);
    }
}
