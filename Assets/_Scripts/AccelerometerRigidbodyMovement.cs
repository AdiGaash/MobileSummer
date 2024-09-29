using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AccelerometerRigidbodyMovement : MonoBehaviour
{
    // Reference to the Rigidbody component
    private Rigidbody rb;

    // Speed multiplier to control movement sensitivity
   

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the GameObject
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the device has an accelerometer
      
            // Get the accelerometer data (x, y, z)
            Vector3 acceleration = Input.acceleration;

            // Convert the accelerometer data to a force vector
            Vector3 movement = new Vector3(acceleration.x, 0, acceleration.y);

            // Apply force to the Rigidbody to move the object
            rb.AddForce(movement);
        
    }
}