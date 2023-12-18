using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class GenerateRandomObstacle : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    //chance to spawn
    public int chanceToSpawn = 75;
    
    //after instantiating the obstacle, destroy the spawner
    void Update()
    {
        Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        //chose a random obstacle
        int randomObstacle = Random.Range(0, obstaclePrefabs.Length);
        //we dont need to raycast because we are not using the ground
        //instantiate the obstacle about 75% of the time
        if (Random.Range(0, 100) < chanceToSpawn)
        {
            GameObject obstacle = Instantiate(obstaclePrefabs[randomObstacle], transform.position, Quaternion.identity);
        } else
        {
            return;
        }

    }


}
