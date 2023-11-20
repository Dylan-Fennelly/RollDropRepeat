using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "ScriptableObjects/QTE/QTE_Data", fileName = "QTE_Data")]
    public class QTE_Data : ScriptableObject
    {
        public float goal;
        public float rockSpeed;
        public Vector2 averageTime;
        public Vector2 otherMovement;
        public Vector2 otherRange;
        public float markMargin;
        public float movementRegenerateTime;
        public float movementTick;
    }
}