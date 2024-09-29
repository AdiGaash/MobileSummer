using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TwoFingerRotationWithEndOnDirectionChange : MonoBehaviour
{
    // Reference to the Image component
    public Image dailImage;

    public TextMeshProUGUI text;
    // Array to hold the correct angle code sequence
    public float[] angleCode;

    // Reference to the AudioSource component
    public AudioSource audioSource;

    // The angle of the rotation
    private float rotationAngle = 0f;

    // Store the initial angle between two touches
    private float initialTouchAngle;

    // Track whether we are currently rotating
    private bool isRotating = false;

    // To track the current digit in the code sequence
    private int codeDigitCounter = 0;

    // Define a small errorTolerance for angle comparison (3 degrees)
    public float errorTolerance = 3f;

    // Cooldown management
    private bool inputCooldown = false;

    void Update()
    {
        // Ignore touch input if we're in cooldown
        if (inputCooldown)
        {
            return;
        }

        // Detect if there are two touches
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // If touches have just begun, initialize rotation
            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                isRotating = true;
                initialTouchAngle = GetAngleBetweenTouches(touch1, touch2);
            }

            // If fingers are moving, calculate the new rotation angle
            if (isRotating && (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved))
            {
                float currentTouchAngle = GetAngleBetweenTouches(touch1, touch2);
                float angleDelta = currentTouchAngle - initialTouchAngle;

                // Update the rotation angle and the dial image
                rotationAngle += angleDelta;
                dailImage.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

                // Update the initial touch angle for continuous movement
                initialTouchAngle = currentTouchAngle;
            }
        }

        // When rotation ends (fingers are lifted)
        if (Input.touchCount < 2 && isRotating)
        {
            EndRotation();
            CheckCode(rotationAngle); // Check the angle after rotation ends
        }
    }

    // Function to check if the rotation angle matches the current angle code
    private void CheckCode(float currentAngle)
    {
        // Normalize the rotation angle to be between 0 and 360 degrees
        currentAngle = NormalizeAngle(currentAngle);

        Debug.Log("currentAngle: " + currentAngle);
        // Check if the current angle is within errorTolerance of the expected code angle
        if (Mathf.Abs(currentAngle - angleCode[codeDigitCounter]) <= errorTolerance)
        {
            Debug.Log("Correct angle: " + angleCode[codeDigitCounter]);

            // Move to the next digit in the code
            codeDigitCounter++;

            // If the last digit has been matched, print "open"
            if (codeDigitCounter >= angleCode.Length)
            {
                text.text = "Open";
                codeDigitCounter = 0; // Reset after successful code entry
            }
            else
            {
                text.text = "so far, OK";
            }

            // Start the cooldown coroutine
            StartCoroutine(InputCooldownCoroutine());
        }
        else
        {
            // If the angle is incorrect, reset the counter
            text.text ="Incorrect, Resetting code";
            codeDigitCounter = 0;
        }
    }

    // Coroutine to handle the input cooldown
    private IEnumerator InputCooldownCoroutine()
    {
        inputCooldown = true; // Set cooldown flag to true
        yield return new WaitForSeconds(1f); // Wait for 1 second
        inputCooldown = false; // Reset cooldown flag
        text.text = "";
    }

    // Function to normalize the angle between 0 and 360 degrees
    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle < 0)
        {
            angle += 360;
        }
        return angle;
    }

    // Function to end the rotation and play audio if necessary
    private void EndRotation()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        isRotating = false;
    }

    // Calculate the angle between two touches
    private float GetAngleBetweenTouches(Touch touch1, Touch touch2)
    {
        Vector2 diff = touch2.position - touch1.position;
        return Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
    }
}
