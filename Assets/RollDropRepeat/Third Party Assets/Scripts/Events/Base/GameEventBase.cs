using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GameEventBase<T> : ScriptableObject
{
    private List<GameEventListenerBase<T>> listeners
        = new List<GameEventListenerBase<T>>();

    [ContextMenu("Raise Event")]
    public virtual void Raise(T data)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(data);
        }
    }

    public void RegisterListener(GameEventListenerBase<T> listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListenerBase<T> listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
