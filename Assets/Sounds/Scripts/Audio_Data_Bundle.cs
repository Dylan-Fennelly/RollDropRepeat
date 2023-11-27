using Events.GameEvents;
using UnityEngine;

namespace Sounds.Scripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Audio/Audio Data Bundle", fileName = "Audio_Data_Bundle")]
    public class Audio_Data_Bundle : ScriptableObject
    {
        public Audio_Events audioEvents;
        public Audio_Data MusicData;
        public Audio_Data SFXData;
        public Audio_Data UIData;
    }
}