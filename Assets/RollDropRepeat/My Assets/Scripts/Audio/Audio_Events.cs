using System.Collections;
using System.Collections.Generic;
using Events.GameEvents;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Events", fileName = "Audio_Events")]
public class Audio_Events : ScriptableObject
{
    public EmptyGameEvent resetSound;
    public AudioGameEvent playSound;
}
