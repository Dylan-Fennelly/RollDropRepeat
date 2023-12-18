using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace DefaultNamespace
{
    public class QT_Balance : QuickTimeEventWithMark
    {
        public override void StartQTE()
        {
            base.StartQTE();
            RockPosition = 50;
            MarkPosition = 50;
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