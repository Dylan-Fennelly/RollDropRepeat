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

    public void HandleEncounter()
    {
        encounterEvent.Raise(data);
        GameObject.Destroy(gameObject);
    }
}
