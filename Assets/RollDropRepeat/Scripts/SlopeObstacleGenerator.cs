using UnityEngine;
using UnityEngine.Rendering;

public class SlopeObstacleGenerator : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float offset = 1.5f;
    public float rotationAmount = 90f;

    void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        // Generate random position around the empty's transform
        float randomX = Random.Range(-1f, 1f);

        // Raycast to find the ground position
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(randomX, 0f, 0f), Vector3.down, out hit))
        {
            // Instantiate obstacle at the point of contact with the ground
            GameObject obstacle = Instantiate(obstaclePrefab, hit.point+ new Vector3(0f, offset, 0f), Quaternion.identity);

            // Rotate obstacle to align with slope normal
            obstacle.transform.up = hit.normal;

            // Rotate the obstacle by 90 degrees around its up axis
            obstacle.transform.Rotate(Vector3.up, rotationAmount);
        }
    }
}
