using UnityEngine;

public class ShakeDetector : MonoBehaviour
{
    // Sensitivity of the shake detection
    public float shakeDetectionThreshold = 2.0f;

    // Minimum interval between shakes in seconds
    public float minShakeInterval = 0.5f;

    // Stores the last shake time
    private float lastShakeTime;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the last shake time
        lastShakeTime = Time.time;

        // Set the shake detection threshold (optional: depends on device sensitivity)
        shakeDetectionThreshold *= shakeDetectionThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        // Get the acceleration value from the accelerometer
        Vector3 acceleration = Input.acceleration;
        

        // Calculate the square magnitude of the acceleration
        float accelerationSqrMagnitude = acceleration.sqrMagnitude;

        // Check if the acceleration exceeds the shake detection threshold
        if (accelerationSqrMagnitude >= shakeDetectionThreshold && Time.time >= lastShakeTime + minShakeInterval)
        {
            // Update the last shake time
            lastShakeTime = Time.time;

            // Call the method to handle the shake event
            OnShake();
        }
    }

    // Method to handle the shake event
    private void OnShake()
    {
        // Add your shake handling code here
        Debug.Log("Device shaken!");
        // Example action: Display a message or perform some other action
        // For example: Show a UI message or trigger an in-game event
    }
}