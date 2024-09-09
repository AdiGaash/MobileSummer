using UnityEngine;

public class ChangePositionOnOrientationChange : MonoBehaviour
{
    // Public transforms for target positions for each orientation
    public Transform portraitPosition;
    public Transform landscapeLeftPosition;
    public Transform landscapeRightPosition;
    public Transform portraitUpsideDownPosition;

    // Store the current orientation
    private ScreenOrientation currentOrientation;

    // Reference to the GameObject that should move (you can assign this in the inspector)
    public GameObject objectToMove;

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
            // Move the GameObject to the new position
            MoveObjectToNewPosition(currentOrientation);
        }
    }

    // This function handles moving the GameObject based on the orientation
    private void MoveObjectToNewPosition(ScreenOrientation newOrientation)
    {
        switch (newOrientation)
        {
            case ScreenOrientation.Portrait:
                if (portraitPosition != null)
                {
                    objectToMove.transform.position = portraitPosition.position;
                    Debug.Log("Moved to Portrait position");
                }
                break;

            case ScreenOrientation.LandscapeLeft:
                if (landscapeLeftPosition != null)
                {
                    objectToMove.transform.position = landscapeLeftPosition.position;
                    Debug.Log("Moved to Landscape Left position");
                }
                break;

            case ScreenOrientation.LandscapeRight:
                if (landscapeRightPosition != null)
                {
                    objectToMove.transform.position = landscapeRightPosition.position;
                    Debug.Log("Moved to Landscape Right position");
                }
                break;

            case ScreenOrientation.PortraitUpsideDown:
                if (portraitUpsideDownPosition != null)
                {
                    objectToMove.transform.position = portraitUpsideDownPosition.position;
                    Debug.Log("Moved to Portrait Upside Down position");
                }
                break;

            default:
                Debug.Log("Unknown or Auto-rotation - no position change");
                break;
        }
    }
}
