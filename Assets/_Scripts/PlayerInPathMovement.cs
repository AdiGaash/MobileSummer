using UnityEngine;

// Enum to represent the possible positions on the grid
public enum PlayerPosition
{
    Left,
    Center,
    Right,
    LeftUp,
    CenterUp,
    RightUp
}

public class PlayerInPathMovement : MonoBehaviour
{
 // Movement speed (higher value = faster movement)
    public float moveSpeed = 5f;

    // Timer for the jump (how long to stay at the upper position before coming back down)
    public float jumpDuration = 1.0f;
    private float jumpTimer;

    // Flag to indicate if the player is currently jumping
    private bool isJumping = false;

    // A flag to check if we are currently moving
    private bool isMoving = false;

    // Store the current position as an enum
    public PlayerPosition currentPosition = PlayerPosition.Center;

    // Store the target position
    private PlayerPosition targetPosition;

    // Vector3 values for each position on the grid
    private Vector3 localLeft = new Vector3(-1, 0, 0);
    private Vector3 localCenter = new Vector3(0, 0, 0);
    private Vector3 localRight = new Vector3(1, 0, 0);

    private Vector3 localLeftUp = new Vector3(-1, 1, 0);
    private Vector3 localCenterUp = new Vector3(0, 1, 0);
    private Vector3 localRightUp = new Vector3(1, 1, 0);

    // Store the horizontal position for returning after jump
    private PlayerPosition originalHorizontalPosition;

    void Start()
    {
        // Initialize the player at the local center position
        transform.localPosition = PositionToVector3(currentPosition);
    }

    void Update()
    {
        // Handle input only if the player is not already moving or jumping
        if (!isMoving && !isJumping)
        {
            HandleInput();
        }

        // Smoothly move the player to the target position
        if (transform.localPosition != PositionToVector3(targetPosition))
        {
            MoveToTarget();
        }

        // Handle jump timing and return to the original position
        if (isJumping)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0)
            {
                // After the jump duration, move the player back down
                MoveDownAfterJump();
            }
        }
    }

    // Function to handle player input and move accordingly in local space
    private void HandleInput()
    {
        // Example using arrow keys
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
    }

    // Move player to the left in local space if possible
    private void MoveLeft()
    {
        if (currentPosition == PlayerPosition.Center)
        {
            targetPosition = PlayerPosition.Left;
        }
        else if (currentPosition == PlayerPosition.Right)
        {
            targetPosition = PlayerPosition.Center;
        }

        StartMoving();
    }

    // Move player to the right in local space if possible
    private void MoveRight()
    {
        if (currentPosition == PlayerPosition.Center)
        {
            targetPosition = PlayerPosition.Right;
        }
        else if (currentPosition == PlayerPosition.Left)
        {
            targetPosition = PlayerPosition.Center;
        }

        StartMoving();
    }

    // Jump to the upper position and come back down after a short delay
    private void Jump()
    {
        originalHorizontalPosition = currentPosition;

        if (currentPosition == PlayerPosition.Left)
        {
            targetPosition = PlayerPosition.LeftUp;
        }
        else if (currentPosition == PlayerPosition.Center)
        {
            targetPosition = PlayerPosition.CenterUp;
        }
        else if (currentPosition == PlayerPosition.Right)
        {
            targetPosition = PlayerPosition.RightUp;
        }

        StartMoving();

        isJumping = true;  // Start the jump process
        jumpTimer = jumpDuration;  // Set the jump timer
    }

    // Automatically move the player back down after the jump duration
    private void MoveDownAfterJump()
    {
        targetPosition = originalHorizontalPosition;
        isJumping = false;
        StartMoving();
    }

    // Function to smoothly move towards the target position using Lerp
    private void MoveToTarget()
    {
        Vector3 targetPos = PositionToVector3(targetPosition);

        // Interpolate towards the target position
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * moveSpeed);

        // If close enough to the target, snap to the target and stop moving
        if (Vector3.Distance(transform.localPosition, targetPos) < 0.1f)
        {
            transform.localPosition = targetPos;
            currentPosition = targetPosition;
            isMoving = false;  // Movement is complete
        }
    }

    // Helper method to start the movement
    private void StartMoving()
    {
        isMoving = true;
    }

    // Convert the enum position to the corresponding Vector3 position
    private Vector3 PositionToVector3(PlayerPosition position)
    {
        switch (position)
        {
            case PlayerPosition.Left: return localLeft;
            case PlayerPosition.Center: return localCenter;
            case PlayerPosition.Right: return localRight;
            case PlayerPosition.LeftUp: return localLeftUp;
            case PlayerPosition.CenterUp: return localCenterUp;
            case PlayerPosition.RightUp: return localRightUp;
            default: return localCenter;
        }
    }
}