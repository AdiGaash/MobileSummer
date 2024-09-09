using UnityEngine;

public class DetectFaceUpOrDown : MonoBehaviour
{
    // Threshold for detecting face up or face down
    public float threshold = 0.8f;

    void Update()
    {
        // Get the current acceleration data from the accelerometer
        Vector3 acceleration = Input.acceleration;

        // Detect if the phone is face up (Z > threshold)
        if (acceleration.z > threshold)
        {
            Debug.Log("Phone is face up.");
        }
        // Detect if the phone is face down (Z < -threshold)
        else if (acceleration.z < -threshold)
        {
            Debug.Log("Phone is face down.");
        }
        else
        {
            Debug.Log("Phone is neither face up nor face down.");
        }
    }
}