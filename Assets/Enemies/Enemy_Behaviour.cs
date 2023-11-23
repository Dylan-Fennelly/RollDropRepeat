
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
        if (data.hasRequirement)
        {
            willEvent.Raise(data.altGuiltGain);
            guiltEvent.Raise(data.altGuiltGain);
            Debug.Log("Encounter with requirement");
        }
        else
        {
            willEvent.Raise(data.willGain);
            guiltEvent.Raise(data.guiltGain);
            Debug.Log("Encounter");
        }
    }
    
    
}
