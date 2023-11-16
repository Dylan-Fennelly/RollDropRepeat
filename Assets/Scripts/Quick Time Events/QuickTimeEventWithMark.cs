using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public abstract class QuickTimeEventWithMark : QuickTimeEvent
    {
        protected float MarkPosition;
        
        protected float OtherSpeed;
        
        protected float TimeElapsed = 0;
        protected float TimeToMove = 0;

        [SerializeField]
        protected Slider markSlider;
        
        protected abstract void CheckProgress();
        protected abstract void RegenerateMovement();
        protected abstract void MoveOther();

        protected override void HandleMovement()
        {
            RegenerateMovement();
            MoveOther();
            base.HandleMovement();
            KeepInBounds();
        }

        protected virtual void KeepInBounds()
        {
            if (RockPosition > 100)
            {
                RockPosition = 100;
            }
            else if (RockPosition < 0)
            {
                RockPosition = 0;
            }
            
            if (MarkPosition > data.otherRange.y)
            {
                MarkPosition = data.otherRange.y;
            }
            else if (MarkPosition < data.otherRange.x)
            {
                MarkPosition = data.otherRange.x;
            }
        }
    }
}