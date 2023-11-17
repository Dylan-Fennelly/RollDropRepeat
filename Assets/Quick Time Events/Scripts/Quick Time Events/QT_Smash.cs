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
                RockPosition = Progress / data.goal;
            }
        }
    }
}