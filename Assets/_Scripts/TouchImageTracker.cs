using UnityEngine;
using UnityEngine.UI;

public class TouchImageTracker : MonoBehaviour
{
    public Image startImage;  // The first image (starting point)
    public Image endImage;    // The second image (destination)
    public Image[] otherImages; // Array of other images that cause failure if touched

    private bool isTracking = false;  // Flag to track if the touch should be followed

    void Update()
    {
        // Check if there's any touch happening
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);  // Get the first touch

            // Check if the touch phase is starting (the touch has just begun)
            if (touch.phase == TouchPhase.Began)
            {
                // Check if the touch started on the start image
                if (IsTouchOnImage(startImage, touch))
                {
                    isTracking = true;  // Start tracking the touch
                }
            }

            // If tracking is active, follow the touch movement
            if (isTracking)
            {
                // Stop tracking if touch hits any of the "other" images
                if (IsTouchOnAnyOtherImage(touch))
                {
                    Debug.Log("Failed: Touch hit a forbidden image!");
                    isTracking = false;
                    return; // Stop further processing
                }

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    // Update some UI or visual feedback here if needed (e.g., show line or indicator)
                }

                // If touch ends or the second image is touched, stop tracking
                if (touch.phase == TouchPhase.Ended || IsTouchOnImage(endImage, touch))
                {
                    isTracking = false;  // Stop tracking
                    if (IsTouchOnImage(endImage, touch))
                    {
                        Debug.Log("Success: Touch moved from start image to end image!");
                        // Trigger desired action here
                    }
                }
            }
        }
    }

    // Helper function to check if the touch is on a specific image
    bool IsTouchOnImage(Image image, Touch touch)
    {
        RectTransform rectTransform = image.GetComponent<RectTransform>();
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, touch.position, null, out localPoint);
        return rectTransform.rect.Contains(localPoint);
    }

    // Helper function to check if the touch is on any other forbidden image
    bool IsTouchOnAnyOtherImage(Touch touch)
    {
        foreach (Image otherImage in otherImages)
        {
            if (IsTouchOnImage(otherImage, touch))
            {
                return true;  // Touch is on one of the forbidden images
            }
        }
        return false;  // No forbidden images were touched
    }
}