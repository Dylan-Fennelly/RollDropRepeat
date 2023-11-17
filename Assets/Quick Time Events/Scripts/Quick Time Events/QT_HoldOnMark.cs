using System;

namespace DefaultNamespace
{
    public class QT_HoldOnMark : QuickTimeEventWithMark
    {
        private void Awake()
        {
            MarkPosition = 70;
        }

        protected override void MoveOther()
        {
            if (TimeToMove > data.movementTick)
            {
                MarkPosition += OtherSpeed;
                TimeToMove = 0;
            }
            else
            {
                TimeToMove += UnityEngine.Time.deltaTime;
            }
        }
        
        protected override void HandleMovement()
        {
            base.HandleMovement();
            markSlider.value = MarkPosition;
        }
    }
}