using Events.Base;
using Events.GameEvents;
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
    //range
    [Range(0f, 45f)]
    public float rotationAngle = 10f; // Rotation angle threshold

    [FoldoutGroup("Animation variables")]
    [SerializeField]
    private GameObject rock;
    [FoldoutGroup("Animation variables")]
    [SerializeField]
    private Animator animator;
    
    [FoldoutGroup("Will Settings")]
    [SerializeField]
    private  FloatGameEvent willEvent;
    
    [FoldoutGroup("Will Settings")]
    [SerializeField]
    private float willAmountStanding = -2f;
    
    [FoldoutGroup("Will Settings")]
    [SerializeField]
    private float willAmountMoving = -1f;
    
    [FoldoutGroup("Audio Settings")]
    [SerializeField]
    private EmptyGameEvent movementSoundsEvent;
    
    [FoldoutGroup("Audio Settings")]
    [SerializeField]
    private EmptyGameEvent stopMovementSoundsEvent;
    
    

    private CharacterController characterController;
    private bool isCollidingWithWall = false;
    private Quaternion targetRotation;
    private bool isRotating = false;
    public bool canMove = false;

    private static readonly int Movement = Animator.StringToHash("Movement");

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            animator.SetFloat(Movement, Input.GetAxis("Vertical"));
            MovePlayer();
            RotatePlayer();
            RotateRock();
        }
    }

    private void RotateRock()
    {
        float horizontalInput = Input.GetAxis("Vertical");
        float verticalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput > 0f)
        {
            if (verticalInput > 0f)
            {
                if (verticalInput > 2f)
                {
                    verticalInput = 2f;
                }
                rock.transform.Rotate(0.2f, 0, verticalInput * 0.05f);
            }
            else
            {
                rock.transform.Rotate(0.2f, 0, 0);
            }
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
                movement.z += Physics.gravity.z * Time.deltaTime; // Apply gravity
                characterController.Move(movement);

                // Set rotation target only if moving forward
                targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                isRotating = true;
            }
            else
            {
                isRotating = false; // Stop rotating when not moving forward
            }
            willEvent.Raise(willAmountMoving*Time.deltaTime);
            movementSoundsEvent.Raise(new Empty());
        }
        else
        {
            isRotating = false; // Reset rotation when not moving
            willEvent.Raise(willAmountStanding*Time.deltaTime);
            stopMovementSoundsEvent.Raise(new Empty());
        }
    }

    private void RotatePlayer()
    {
        if (isRotating)
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            // Calculate the target rotation based on the player's horizontal movement
            Vector3 direction = new Vector3(horizontalInput, 0f, 1f).normalized;
            targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            float step = rotationSpeed * Time.deltaTime;

            // Directly set the rotation towards the targetRotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

            // Check if the current rotation exceeds the specified angle
            float currentRotationAngle = Mathf.DeltaAngle(0f, transform.eulerAngles.y);
            if (Mathf.Abs(currentRotationAngle) > rotationAngle)
            {
                // Snap to the exact rotation limit
                transform.rotation = Quaternion.Euler(0f, Mathf.Sign(currentRotationAngle) * rotationAngle, 0f);

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
