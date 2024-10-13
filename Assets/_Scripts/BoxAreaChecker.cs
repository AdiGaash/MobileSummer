using UnityEngine;

public class BoxAreaChecker : MonoBehaviour
{
    // LayerMask to check which layers to detect
    public LayerMask detectionLayer;
    
    // Box dimensions relative to the object this script is attached to
    public Vector3 boxCenter = Vector3.zero; // The center of the box relative to the GameObject
    public Vector3 boxSize = new Vector3(5, 5, 5); // The size of the box in X, Y, Z

    // Update is called once per frame
    void Update()
    {
        // Perform a box overlap check to find all colliders within the defined box
        Collider[] objectsInBox = Physics.OverlapBox(transform.position + boxCenter, boxSize / 2, Quaternion.identity);

        // Iterate through all detected objects
        foreach (Collider obj in objectsInBox)
        {
            // Check if the object is in the specified layer mask
            if (IsInLayerMask(obj.gameObject, detectionLayer))
            {
                // Object is inside the box and on the specified layer
                Debug.Log("GameObject: " + obj.gameObject.name + " is inside the box and on the correct layer.");
            }
        }
    }

    // Helper function to check if the GameObject is in the specified layer mask
    bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return (mask.value & (1 << obj.layer)) != 0;
    }

    // To visualize the box in the scene editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green; // Set the color to green for visualizing
        Gizmos.DrawWireCube(transform.position + boxCenter, boxSize); // Draw the box as a wireframe
    }
}