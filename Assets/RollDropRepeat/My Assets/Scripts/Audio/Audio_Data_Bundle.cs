using Events.GameEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sounds.Scripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Data Bundle", fileName = "Audio_Data_Bundle")]
    public class Audio_Data_Bundle : ScriptableObject
    {
        [Tooltip("The ScriptableObject audio events to use")]
        public Audio_Events audioEvents;
        [Tooltip("The audio data of the music")] [BoxGroup("Audio Data Objects")]
        public Audio_Data MusicData;
        [Tooltip("The audio data of the general sound effects")] [BoxGroup("Audio Data Objects")]
        public Audio_Data SFXData;
        [Tooltip("The audio data of the sissy")] [BoxGroup("Audio Data Objects")]
        public Audio_Data SissyData;
        [Tooltip("The audio data of the rock")] [BoxGroup("Audio Data Objects")]
        public Audio_Data RockData;
        [Tooltip("The audio data of the UI")] [BoxGroup("Audio Data Objects")]
        public Audio_Data UIData;
    }
}