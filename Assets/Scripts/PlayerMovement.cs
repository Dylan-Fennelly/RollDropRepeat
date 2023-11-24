using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviour
{
    [FoldoutGroup("Movement Settings")]
    [LabelText("Lane Width")]
    public float laneWidth = 2f; // Width of each lane

    [FoldoutGroup("Movement Settings")]
    [LabelText("Move Speed")]
    public float moveSpeed = 5f; // Player movement speed

    private CharacterController characterController;
    private bool isMoving = false;
    public bool canMove = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // If the player is not currently moving, allow input
        if (!isMoving && canMove)
        {
            // Handle player movement
            MovePlayer();
        }

    }

    private void MovePlayer()
    {
        // Get digital input for horizontal movement (left or right)
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Check if there is significant input
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            // Calculate target position based on input and lane width
            Vector3 movement = new Vector3(horizontalInput * laneWidth, 0f, 0f);

            // Move the player to the target position using CharacterController
            StartCoroutine(MovePlayerCoroutine(movement));
        }
    }

    private System.Collections.IEnumerator MovePlayerCoroutine(Vector3 movement)
    {
        // Set isMoving to true to prevent additional input during movement
        isMoving = true;

        // Calculate the time it will take to move the entire lane width at the given speed
        float moveTime = laneWidth / moveSpeed;

        float elapsedTime = 0f;

        // Move the player while elapsed time is less than moveTime
        while (elapsedTime < moveTime)
        {
            characterController.Move(movement * Time.deltaTime * moveSpeed);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Set isMoving to false after the movement is completed
        isMoving = false;
    }
}
