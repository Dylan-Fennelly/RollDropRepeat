using System.Collections;
using System.Collections.Generic;
using Events.GameEvents;
using UnityEngine;

public class EncounterTrigger : MonoBehaviour
{
    [SerializeField]
    private Enemy_Data data;
    [SerializeField]
    private EnemyGameEvent encounterEvent;
    
    [SerializeField]
    private ParticleSystem particleSystem;
    
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();

    public void HandleEncounter()
    {
        encounterEvent.Raise(data);
        particleSystem.Play();
        
        foreach (var enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }
}
