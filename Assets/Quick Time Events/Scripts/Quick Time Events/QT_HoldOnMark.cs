using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class QT_HoldOnMark : QuickTimeEventWithMark
    {
        
        public override void StartQTE()
        {
            base.StartQTE();
            MarkPosition = 70;
        }
        
        protected override void HandleInput()
        {
            if (Input.GetKey(KeyCode.D))
            {
                RockPosition += data.rockSpeed;
            }
            else
            {
                RockPosition -= data.rockSpeed;
            }
            
            CheckProgress();
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