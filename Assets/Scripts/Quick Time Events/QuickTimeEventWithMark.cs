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

        
        protected abstract void MoveOther();

        protected override void HandleMovement()
        {
            RegenerateMovement();
            MoveOther();
            base.HandleMovement();
            KeepInBounds();
        }
        
        private void RegenerateMovement()
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
        
        protected bool CheckProgress()
        {
            if (RockPosition <= MarkPosition + data.markMargin && RockPosition >= MarkPosition - data.markMargin)
            {
                Progress++;
                return true;
            }

            return false;
        }
    }
}