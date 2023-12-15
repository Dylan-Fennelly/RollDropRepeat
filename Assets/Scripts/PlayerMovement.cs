using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviour
{
    [FoldoutGroup("Movement Settings")]
    [LabelText("Move Speed")]
    public float moveSpeed = 5f; // Player movement speed

    [FoldoutGroup("Rotation Settings")]
    [LabelText("Rotation Speed")]
    public float rotationSpeed = 300f; // Increased rotation speed
    [FoldoutGroup("Rotation Settings")]
    [LabelText("Rotation Angle")]
    public float rotationAngle = 10f; // Rotation angle threshold

    private CharacterController characterController;
    private bool isCollidingWithWall = false;
    private Quaternion targetRotation;
    private bool isRotating = false;
    public bool canMove = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (canMove)
        {
            MovePlayer();
            RotatePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") && !isCollidingWithWall)
        {
            // Reverse the direction of movement
            ReverseMovement();
        }
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Allow left and right movement only when moving forward
            if (verticalInput > 0f)
            {
                // Move the player based on the direction and gravity
                Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized * moveSpeed * Time.deltaTime;
                movement.y += Physics.gravity.y * Time.deltaTime; // Apply gravity
                characterController.Move(movement);

                // Set rotation target only if moving forward
                targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                isRotating = true;
            }
            else
            {
                isRotating = false; // Stop rotating when not moving forward
            }
        }
        else
        {
            isRotating = false; // Reset rotation when not moving
        }
    }
    private void RotatePlayer()
    {
        if (isRotating)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            // Calculate the target rotation based on the player's horizontal movement
            Vector3 direction = new Vector3(horizontalInput, 0f, 0f).normalized;

            if (direction.magnitude >= 0.1f)
            {
                targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            }

            float step = rotationSpeed * Time.deltaTime;

            // Directly set the rotation towards the targetRotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

            // Check if the current rotation is very close to the targetRotation
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                // Snap to the exact targetRotation
                transform.rotation = targetRotation;

                // Stop rotating
                isRotating = false;
            }
        }
    }

    private void ReverseMovement()
    {
        isCollidingWithWall = true;

        Vector3 reverseDirection = -characterController.velocity.normalized; // Reverse the current velocity
        Vector3 reverseMovement = reverseDirection * moveSpeed * Time.deltaTime;

        while (characterController.isGrounded)
        {
            characterController.Move(reverseMovement);
        }

        isCollidingWithWall = false;
    }

    public void StopMovement()
    {
        canMove = false;
    }

    public void ResumeMovement()
    {
        canMove = true;
    }
}
