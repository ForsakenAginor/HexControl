namespace BotLogic
{
    internal class ToReturningTransition : Transition
    {
        internal void SetTargetState(ReturningState state)
        {
            base.SetTargetState(state);
        }
    }
}