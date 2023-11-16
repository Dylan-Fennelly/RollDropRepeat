namespace DefaultNamespace
{
    public class QT_HoldOnMark : QuickTimeEventWithMark
    {
        protected override void HandleInput()
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        protected override void MoveOther()
        {
            throw new System.NotImplementedException();
        }
    }
}