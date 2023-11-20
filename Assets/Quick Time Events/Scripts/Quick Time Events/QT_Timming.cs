using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class QT_Timming : QuickTimeEventWithMark
    {
        private int direction = 1;
        private bool canScore = true;
        
        public override void StartQTE()
        {
            base.StartQTE();
            RockPosition = 0;
            MarkPosition = Random.Range(data.otherRange.x, data.otherRange.y);
        }

        protected override void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.W) && canScore)
            {
                CheckProgress();
                canScore = false;
            }
        }

        protected override void MoveOther()
        {
            if (TimeToMove > data.movementTick)
            {
                RockPosition += OtherSpeed * direction;
                TimeToMove = 0;
            }
            else
            {
                TimeToMove += UnityEngine.Time.deltaTime;
            }
        }

        protected override void KeepInBounds()
        {
            if (RockPosition > 100)
            {
                direction = -1;
                canScore = true;
            }
            else if (RockPosition < 0)
            {
                direction = 1;
                canScore = true;
            }
        }
        
        protected override void HandleMovement()
        {
            base.HandleMovement();
            markSlider.value = MarkPosition;
        }
    }
}