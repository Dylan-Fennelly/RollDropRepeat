using System.Collections;
using System.Collections.Generic;
using Events.GameEvents;
using Sirenix.OdinInspector;
using Sounds.Scripts;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Data", fileName = "Audio_Data")]
public class Audio_Data : ScriptableObject
{
    public AudioClip clip;
    [EnumPaging] public SourceAudioType type;
}
