using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public GameObject groundPrefab;
    public List<GameObject> groundPrefabs; // List of ground prefabs
    public float angleBetweenGrounds = 20f;
    public float deletionOffsetMultiplier = 2f;
    public float movementSpeed = 5f;
    public Vector3 fixedOffset = new Vector3(0, 4.45f, 17.32f);
    [SerializeField]
    private Vector3 initialPosition;

    private List<Transform> groundTransforms = new List<Transform>();

    private void Start()
    {
        // Initial instantiation of ground pieces
        InstantiateGround(groundPrefab, initialPosition, Quaternion.Euler(-15f, 0f, 0f));
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
            // Instantiate a random ground prefab at the end of the previous ones with the fixed offset
            InstantiateGround(groundPrefabs[Random.Range(0, groundPrefabs.Count)], lastGround.position + fixedOffset, lastGround.rotation);
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
        float angleRad = angleBetweenGrounds * Mathf.Deg2Rad;
        return new Vector3(0, -Mathf.Sin(angleRad), -Mathf.Cos(angleRad)).normalized;
    }

    private float CalculateDeletionOffset()
    {
        return CalculateGroundDistance(groundPrefabs[0]) * deletionOffsetMultiplier; // Assuming all prefabs have the same length
    }

    private float CalculateGroundDistance(GameObject groundPrefab)
    {
        return groundPrefab.transform.localScale.z;
    }
}
