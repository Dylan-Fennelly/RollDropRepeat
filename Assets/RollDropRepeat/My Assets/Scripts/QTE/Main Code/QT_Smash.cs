using UnityEngine;

namespace DefaultNamespace
{
    public class QT_Smash : QuickTimeEvent
    {
        protected override void HandleInput()
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                Progress++;
                PlayUISound();
                RockPosition = Progress / data.goal;
            }
        }

        public override void StartQTE()
        {
            base.StartQTE();
            RockPosition = 0;
            rockSlider.value = RockPosition;
        }
    }
}