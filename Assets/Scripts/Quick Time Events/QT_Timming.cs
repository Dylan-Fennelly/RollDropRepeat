using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class QT_Timming : QuickTimeEventWithMark
    {
        private int fail = 0;
        private int direction = 1;
        private bool canScore = true;

        private void Awake()
        {
            MarkPosition = Random.Range(data.otherRange.x, data.otherRange.y);
        }

        protected override void HandleInput()
        {
            if (Input.GetKeyUp(KeyCode.W) && canScore)
            {
                if (!CheckProgress()) fail++;
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