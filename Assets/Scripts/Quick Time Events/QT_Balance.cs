using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DefaultNamespace
{
    public class QT_Balance : QuickTimeEventWithMark
    {
        private void Awake()
        {
            RockPosition = 50;
            MarkPosition = 50;
        }

        protected override void HandleInput()
        {
            if (Input.GetKey(KeyCode.A))
            {
                RockPosition -= data.rockSpeed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                RockPosition += data.rockSpeed;
            }
            
            CheckProgress();
        }

        protected override void CheckProgress()
        {
            if (RockPosition <= MarkPosition + data.markMargin && RockPosition >= MarkPosition - data.markMargin)
            {
                Progress++;
            }
        }

        protected override void RegenerateMovement()
        {
            if (TimeElapsed > data.movementRegenerateTime)
            {
                TimeElapsed = 0;
                OtherSpeed = Random.Range(data.otherMovement.x, data.otherMovement.y);
            }
            else
            {
                TimeElapsed += UnityEngine.Time.deltaTime;
            }
        }

        protected override void MoveOther()
        {
            if (TimeToMove > data.movementTick)
            {
                RockPosition += OtherSpeed;
                TimeToMove = 0;
            }
            else
            {
                TimeToMove += UnityEngine.Time.deltaTime;
            }
        }
        
        protected override void KeepInBounds()
        {
            if (RockPosition > data.otherRange.y)
            {
                RockPosition = data.otherRange.y;
            }
            else if (RockPosition < data.otherRange.x)
            {
                RockPosition = data.otherRange.x;
            }
        }
    }
}