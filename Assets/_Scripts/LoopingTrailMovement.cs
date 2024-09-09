using UnityEngine;

public class LoopingTrailMovement : MonoBehaviour
{
    public Transform[] pathPoints; // Array of points forming the trail
    public float speed = 5f; // Movement speed
    private int currentPointIndex = 0; // Current point index along the path
    private float lerpValue = 0f; // Interpolation value between two points

    void Update()
    {
        if (pathPoints.Length == 0) return;

        // Move the object along the current segment
        Transform startPoint = pathPoints[currentPointIndex];
        Transform endPoint = pathPoints[(currentPointIndex + 1) % pathPoints.Length]; // Loop back to the first point
        
        // Interpolate smoothly between two points
        lerpValue += Time.deltaTime * speed / Vector3.Distance(startPoint.position, endPoint.position);
        transform.position = Vector3.Lerp(startPoint.position, endPoint.position, lerpValue);

        // Check if we've reached the next point
        if (lerpValue >= 1f)
        {
            lerpValue = 0f; // Reset lerp value
            currentPointIndex = (currentPointIndex + 1) % pathPoints.Length; // Move to the next point, loop if necessary
        }
    }
}