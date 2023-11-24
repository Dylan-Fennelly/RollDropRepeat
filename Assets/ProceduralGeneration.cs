using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject groundWithWallsPrefab;
    public float angleBetweenGrounds = 20f; // Angle between ground pieces
    public float deletionOffsetMultiplier = 2f; // Multiplier for deletion offset
    public float movementSpeed = 5f; // Speed at which the ground moves
    public Vector3 fixedOffset = new Vector3(0, 4.45f, 17.32f); // Serialized offset

    private List<Transform> groundTransforms = new List<Transform>();

    private void Start()
    {
        // Initial instantiation of ground pieces
        InstantiateGround(groundPrefab, Vector3.zero, Quaternion.Euler(-15f, 0f, 0f));
        InstantiateGround(groundWithWallsPrefab, fixedOffset, Quaternion.Euler(-15f, 0f, 0f));
    }

    private void Update()
    {
        MoveGround();
        DeleteGround();
    }

    private void MoveGround()
    {
        foreach (Transform groundTransform in groundTransforms)
        {
            // Move the ground along an angled path
            Vector3 targetPosition = groundTransform.position + CalculateMovementVector(groundTransform) * movementSpeed * Time.deltaTime;
            groundTransform.position = Vector3.MoveTowards(groundTransform.position, targetPosition, movementSpeed * Time.deltaTime);
        }

        // Check if we need to instantiate more ground
        Transform lastGround = groundTransforms[groundTransforms.Count - 1];
        if (lastGround.position.z < CalculateDeletionOffset())
        {
            // Instantiate new ground pieces at the end of the previous ones with the fixed offset
            InstantiateGround(groundWithWallsPrefab, lastGround.position + fixedOffset, lastGround.rotation);
        }
    }

    private void DeleteGround()
    {
        // Delete ground tiles that are out of view
        Transform firstGround = groundTransforms[0];

        if (firstGround.position.z < -CalculateDeletionOffset())
        {
            Destroy(firstGround.gameObject);
            groundTransforms.RemoveAt(0);
        }
    }

    private void InstantiateGround(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        // Instantiate the new ground at the end of the previous one
        GameObject newGround = Instantiate(prefab, position, rotation, transform);
        groundTransforms.Add(newGround.transform);
    }

    private Vector3 CalculateMovementVector(Transform groundTransform)
    {
        // Calculate the movement vector based on the angle between ground pieces
        float angleRad = angleBetweenGrounds * Mathf.Deg2Rad;
        return new Vector3(0, -Mathf.Sin(angleRad), -Mathf.Cos(angleRad)).normalized;
    }

    private float CalculateDeletionOffset()
    {
        // Calculate the deletion offset based on the length of two grounds
        return CalculateGroundDistance(groundPrefab) * deletionOffsetMultiplier;
    }

    private float CalculateGroundDistance(GameObject groundPrefab)
    {
        // Assuming the grounds are aligned along the z-axis
        return groundPrefab.transform.localScale.z;
    }
}
