
using Events.Base;
using UnityEngine;


namespace Events.GameEvents
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Events/Empty", fileName = "EmptyGameEvent")]
    public class EmptyGameEvent : GameEventBase<Empty>
    {
        
    }
}