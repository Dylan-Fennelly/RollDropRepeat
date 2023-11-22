
using Events.GameEvents;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private FloatGameEvent willEvent;
    [SerializeField]
    private FloatGameEvent guiltEvent;
    
    public void HandleEncounter(Enemy_Data data)
    {
        willEvent.Raise(data.willGain);
        guiltEvent.Raise(data.guiltGain);
    }
    
    
}
