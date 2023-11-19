using System.Collections;
using System.Collections.Generic;
using Events.Base;
using Events.GameEvents;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    [SerializeField]
    EmptyGameEvent eventToStart;
    
    public void StartEvent()
    {
        eventToStart.Raise(new Empty());
    }
}
