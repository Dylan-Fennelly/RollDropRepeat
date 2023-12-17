using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Events.Base;
using Events.GameEvents;
using UnityEngine;

public class PlayerTriggerBehaviour : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            other.GetComponent<StartTrigger>().StartEvent();
        }
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EncounterTrigger>().HandleEncounter();
        }
        if (other.CompareTag("Finish"))
        {
            other.GetComponent<WinBehaviour>().Win();
        }
    }
}
