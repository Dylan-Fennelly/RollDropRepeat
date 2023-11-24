// ProceduralGeneration.cs
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public GameObject groundPrefab;
    public List<GameObject> groundPrefabs;
    public float angleBetweenGrounds = 20f;
    public float deletionOffsetMultiplier = 2f;
    public float movementSpeed = 5f;
    public Vector3 fixedOffset = new Vector3(0, 4.45f, 17.32f);
    [SerializeField]
    private Vector3 initialPosition;

    private ProceduralGenerationManager manager;
    private List<Transform> groundTransforms = new List<Transform>();
    private bool canMoveGround = true;

    private void Start()
    {
        InstantiateGround(groundPrefab, initialPosition, Quaternion.Euler(-15f, 0f, 0f));
        manager = FindObjectOfType<ProceduralGenerationManager>();
    }

    private void Update()
    {
        if (canMoveGround)
        {
            MoveGround();
            DeleteGround();
        }
    }

    // Call this method when you want to stop the ground movement (e.g., when the mini-game starts)
    public void StopGroundMovement()
    {
        canMoveGround = false;
    }

    // Call this method when you want to resume the ground movement (e.g., when the mini-game ends)
    public void ResumeGroundMovement()
    {
        canMoveGround = true;
    }

    private void MoveGround()
    {
        foreach (Transform groundTransform in groundTransforms)
        {
            Vector3 targetPosition = groundTransform.position + CalculateMovementVector(groundTransform) * movementSpeed * Time.deltaTime;
            groundTransform.position = Vector3.MoveTowards(groundTransform.position, targetPosition, movementSpeed * Time.deltaTime);
        }

        Transform lastGround = groundTransforms[groundTransforms.Count - 1];
        if (lastGround.position.z < CalculateDeletionOffset())
        {
            InstantiateGround(groundPrefabs[Random.Range(0, groundPrefabs.Count)], lastGround.position + fixedOffset, lastGround.rotation);
        }
    }

    private void DeleteGround()
    {
        Transform firstGround = groundTransforms[0];

        if (firstGround.position.z < -CalculateDeletionOffset())
        {
            Destroy(firstGround.gameObject);
            groundTransforms.RemoveAt(0);
        }
    }

    private void InstantiateGround(GameObject prefab, Vector3 position, Quaternion rotation)
    {
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
        return CalculateGroundDistance(groundPrefabs[0]) * deletionOffsetMultiplier;
    }

    private float CalculateGroundDistance(GameObject groundPrefab)
    {
        return groundPrefab.transform.localScale.z;
    }
}
