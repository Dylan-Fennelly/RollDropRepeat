using System.Collections;
using System.Collections.Generic;
using Events.Base;
using Events.GameEvents;
using UnityEngine;

public class WinBehaviour : MonoBehaviour
{
    [SerializeField]
    private EmptyGameEvent winEvent;

    public void Win()
    {
        winEvent.Raise(new Empty());
    }
}
