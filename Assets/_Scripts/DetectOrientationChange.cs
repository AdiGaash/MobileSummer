using UnityEngine;

public class DetectOrientationChange : MonoBehaviour
{
    // Store the current screen orientation
    private ScreenOrientation currentOrientation;

    void Start()
    {
        // Initialize the current orientation
        currentOrientation = Screen.orientation;
        Debug.Log("Starting orientation: " + currentOrientation);
    }

    void Update()
    {
        // Check if the screen orientation has changed
        if (currentOrientation != Screen.orientation)
        {
            // Update the current orientation
            currentOrientation = Screen.orientation;
            // Call the function to handle the orientation change
            OnOrientationChange(currentOrientation);
        }
    }

    // This function is called when the orientation changes
    private void OnOrientationChange(ScreenOrientation newOrientation)
    {
        // You can handle the orientation change here
        switch (newOrientation)
        {
            case ScreenOrientation.Portrait:
                Debug.Log("Orientation changed to Portrait");
                break;

            case ScreenOrientation.LandscapeLeft:
                Debug.Log("Orientation changed to Landscape Left");
                break;

            case ScreenOrientation.LandscapeRight:
                Debug.Log("Orientation changed to Landscape Right");
                break;

            case ScreenOrientation.PortraitUpsideDown:
                Debug.Log("Orientation changed to Portrait Upside Down");
                break;

            default:
                Debug.Log("Orientation changed to Unknown or Auto-rotation");
                break;
        }
    }
}