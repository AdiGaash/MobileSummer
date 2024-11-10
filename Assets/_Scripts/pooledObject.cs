using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private Vector3 lowerLeft;  // Store the lower left position
    private Vector3 lowerRight; // Store the lower right position

    void Start()
    {
        CalculateLowerPositions(); // Calculate the lower left and right positions once
    }

    private void CalculateLowerPositions()
    {
        // Get the Renderer component attached to the GameObject
        Renderer renderer = GetComponent<Renderer>();

        // If there's no Renderer, log a warning and exit the method
        if (renderer == null)
        {
            Debug.LogWarning("No Renderer found on this GameObject.");
            return;
        }

        // Calculate the lower left and lower right positions based on the renderer's bounds
        lowerLeft = new Vector3(
            renderer.bounds.min.x,
            renderer.bounds.center.y,
            renderer.bounds.center.z
        );

        lowerRight = new Vector3(
            renderer.bounds.max.x,
            renderer.bounds.center.y,
            renderer.bounds.center.z
        );
    }

    // Method to set the position based on lower left or lower right with offset
    public void SetTransform(Vector3 targetPosition, Quaternion rotation, float offset, bool useLowerLeft)
    {
        // Choose lower left or lower right based on the parameter
        Vector3 selectedPosition = useLowerLeft ? lowerLeft : lowerRight;

        // Calculate the new position based on the selected edge
        Vector3 newPosition = targetPosition - selectedPosition;
        
        transform.rotation = rotation;
        transform.Rotate(0,0,90);
        
        // Apply the offset to the left or right
        Vector3 moveDirection;
        if (useLowerLeft)
        {
            moveDirection = transform.TransformDirection(Vector3.left.normalized);
        }
        else
        {
            moveDirection = transform.TransformDirection(Vector3.right.normalized);
        }
        
        // Set the object's position
        transform.position =newPosition + (moveDirection * offset);
            
    }
   
}