using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sounds.Scripts;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Data", fileName = "Audio_Data")]
public class Audio_Data : ScriptableObject
{
    public List<AudioClip> clip;
    [EnumPaging] public SourceAudioType type;
    public bool shouldOverride = false;
    [Range(0,1)]
    public float volume = 1f;
}
