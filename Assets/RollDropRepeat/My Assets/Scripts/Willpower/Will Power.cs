using System.Collections;
using System.Collections.Generic;
using Events.Base;
using Events.GameEvents;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class Willpower : MonoBehaviour
{

    private float _will;
    [SerializeField]
    private FloatGameEvent willEvent;
    
    [SerializeField]
    private EmptyGameEvent gameOverEvent;
    
    [FormerlySerializedAs("maxMotivation")] [SerializeField]
    private float maxWill = 100f;
    // Start is called before the first frame update
    void Start()
    {
        _will = maxWill;
    }
    
    public void AddMotivation(float amount)
    {
        _will += amount;
        if (_will > maxWill)
        {
            _will = maxWill;
        }

        if (_will <= 0)
        {
            Debug.Log("Game Over");
            gameOverEvent.Raise(new Empty());
        }
        willEvent.Raise(_will/maxWill);
        
    }
    
}
