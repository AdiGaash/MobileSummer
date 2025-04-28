using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 targetPosition = new Vector3(0, 10, 0); // Target position
    public float duration = 2.0f; // Duration of the movement

    private Vector3 startPosition;
    private float elapsedTime = 0;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the fraction of the duration that has passed
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / duration;

        // Move the object towards the target position
        transform.position = Vector3.Lerp(startPosition, targetPosition, t);
    }
}