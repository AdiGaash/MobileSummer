using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables to control the player's speed
    public float moveSpeed = 5f;
    
    // Rigidbody component for physics-based movement
    private Rigidbody rb;

    // Store input for movement along the X and Z axes
    private float moveX;
    private float moveZ;

    void Start()
    {
        // Get the Rigidbody component attached to the player object
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get player input from the horizontal (X) and vertical (Z) axes
        moveX = Input.GetAxis("Horizontal"); // Maps to A/D or Left/Right arrow keys
        moveZ = Input.GetAxis("Vertical");   // Maps to W/S or Up/Down arrow keys
    }

    void FixedUpdate()
    {
        // Apply movement based on input and speed
        Vector3 movement = new Vector3(moveX, 0f, moveZ) * moveSpeed * Time.deltaTime;
        
        // Move the player using Rigidbody (physics-based movement)
        rb.MovePosition(rb.position + movement);
    }
}