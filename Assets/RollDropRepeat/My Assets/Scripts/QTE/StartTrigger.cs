using System.Collections;
using System.Collections.Generic;
using Events.Base;
using Events.GameEvents;
using UnityEngine;

public class StartTrigger : MonoBehaviour
{
    [SerializeField]
    EmptyGameEvent eventToStart;
    
    private bool _started = false;
    
    public void StartEvent()
    {
        if (_started) return;
        
        eventToStart.Raise(new Empty());
        gameObject.GetComponent<BoxCollider>().enabled = false; 
        _started = true;
    }
}
