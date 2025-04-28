using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 targetRotation = new Vector3(0, 180, 0); // Target rotation
    public float duration = 2.0f; // Duration of the rotation

    private Quaternion startRotation;
    private Quaternion endRotation;
    private float elapsedTime = 0;

    void Start()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(targetRotation);
    }

    void Update()
    {
        // Calculate the fraction of the duration that has passed
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / duration;

        // Rotate the object towards the target rotation
        transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
    }
}