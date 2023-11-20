using UnityEngine;

namespace DefaultNamespace
{
    public class QT_TwoSmash : QuickTimeEvent
    {
        private bool isA = true;
        private KeyCode[] key = {KeyCode.A, KeyCode.D};
        
        protected override void HandleInput()
        {
            if (Input.GetKeyUp(key[isA ? 0 : 1]))
            {
                Progress++;
                RockPosition = Progress / data.goal;
                isA = !isA;
            }
        }
        
        public override void StartQTE()
        {
            base.StartQTE();
            RockPosition = 0;
        }
    }
}