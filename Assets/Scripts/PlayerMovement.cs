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
    private bool isCollidingWithWall = false;
    private Vector3 originalPosition;
    public bool canMove = true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        CaptureOriginalPosition();
    }

    private void Update()
    {
        if (!isMoving && canMove)
        {
            MovePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") && !isCollidingWithWall)
        {
            // Stop the player immediately
            StopAllCoroutines();

            // Reverse the direction of movement
            StartCoroutine(ReverseMovementCoroutine());
        }
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            CaptureOriginalPosition(); // Capture the original position before each movement
            Vector3 movement = new Vector3(horizontalInput * laneWidth, 0f, 0f);
            StartCoroutine(MovePlayerCoroutine(movement));
        }
    }

    private void CaptureOriginalPosition()
    {
        originalPosition = transform.position;
    }

    private System.Collections.IEnumerator MovePlayerCoroutine(Vector3 movement)
    {
        isMoving = true;

        float moveTime = laneWidth / moveSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            characterController.Move(movement * Time.deltaTime * moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false; // Move this line to the end of the coroutine
    }

    private System.Collections.IEnumerator ReverseMovementCoroutine()
    {
        isCollidingWithWall = true;

        float reverseTime = laneWidth / moveSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < reverseTime)
        {
            transform.position = Vector3.Lerp(originalPosition, transform.position, elapsedTime / reverseTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isCollidingWithWall = false;
        isMoving = false; // Reset the isMoving flag after snapping back
    }
}
