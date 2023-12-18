using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private float smoothness = 5f;

    private PlayerMovement playerMovement;
    private Vector3 initialOffset;

    private void Awake()
    {
        if (playerTransform == null)
        {
            playerTransform = FindObjectOfType<PlayerMovement>().transform;
        }

        playerMovement = playerTransform.GetComponent<PlayerMovement>();

        // Store the initial offset
        initialOffset = transform.position - playerTransform.position;
    }

    private void LateUpdate()
    {
        if (playerTransform != null && playerMovement != null && playerMovement.canMove)
        {
            // Calculate the target position to follow the player
            Vector3 targetPosition = playerTransform.position + initialOffset;

            // Smoothly move the camera towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness * Time.deltaTime);
        }
    }
}
