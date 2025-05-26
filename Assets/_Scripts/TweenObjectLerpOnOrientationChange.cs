using UnityEngine;

public class TweenObjectLerpOnOrientationChange : MonoBehaviour
{
    // Public transforms for target positions for each orientation
    public Transform portraitPosition;
    public Transform landscapePosition;
   
    

    // Store the current orientation
    private ScreenOrientation currentOrientation;

    // Reference to the GameObject that should move (you can assign this in the inspector)
    public GameObject objectToMove;

    // Speed of the Lerp movement
    public float moveSpeed = 2.0f;

    // Target position to move to
    private Vector3 targetPosition;

    // Flag to check if movement is ongoing
    private bool isMoving = false;

    void Start()
    {
        // Initialize the current orientation
        currentOrientation = Screen.orientation;
        SetNewTargetPosition(currentOrientation);
       
    }

    void Update()
    {
        // Check if the screen orientation has changed
        if (currentOrientation != Screen.orientation)
        {
            // Update the current orientation
            currentOrientation = Screen.orientation;
            // Set the new target position based on the orientation
            SetNewTargetPosition(currentOrientation);
            Debug.Log("update UI element positions");
        }

        // Move the object towards the target position using Lerp if it's moving
        if (isMoving)
        {
            objectToMove.transform.position = Vector3.Slerp(objectToMove.transform.position, targetPosition, Time.deltaTime * moveSpeed);

            // Check if the object is close enough to the target to stop moving
            if (Vector3.Distance(objectToMove.transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;  // Stop moving once it's close enough to the target
            }
        }
    }

    // This function handles setting the target position based on the orientation
    private void SetNewTargetPosition(ScreenOrientation newOrientation)
    {
        switch (newOrientation)
        {
            case ScreenOrientation.Portrait:
                    targetPosition = portraitPosition.position;
                    isMoving = true;
                break;

            case ScreenOrientation.LandscapeLeft:
                    targetPosition = landscapePosition.position;
                    isMoving = true;
                break;

            case ScreenOrientation.LandscapeRight:
                    targetPosition = landscapePosition.position;
                    isMoving = true;
                break;

            case ScreenOrientation.PortraitUpsideDown:
                    targetPosition = portraitPosition.position;
                    isMoving = true;
                break;

            default:
                Debug.Log("Unknown or Auto-rotation - no position change");
                isMoving = false;
                break;
        }
    }
}


