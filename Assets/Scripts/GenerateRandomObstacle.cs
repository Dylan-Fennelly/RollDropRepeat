using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class GenerateRandomObstacle : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    //range
    public float offsetLeft = -1.5f;
    public float offsetRight = 1.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        //chose a random obstacle
        int randomObstacle = Random.Range(0, obstaclePrefabs.Length);
        //generate random position in the x axis based on the location of the empty this is based on so it can as much as the leftside offset or the right or in between
        float randomX = Random.Range(offsetLeft, offsetRight);
        //we dont need to raycast because we are not using the ground
        //instantiate the obstacle
        GameObject obstacle = Instantiate(obstaclePrefabs[randomObstacle], transform.position + new Vector3(randomX, 0f, 0f), Quaternion.identity);

    }


}
