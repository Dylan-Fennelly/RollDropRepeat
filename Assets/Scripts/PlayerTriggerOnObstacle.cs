using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Events.Base;
using Events.GameEvents;
using UnityEngine;

public class PlayerTriggerOnObstacle : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            other.GetComponent<StartTrigger>().StartEvent();
        }
    }
}
