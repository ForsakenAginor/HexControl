namespace Assets.Scripts.BotLogic.StateMachine.Transitions
{
    internal class ToMovingTransition : Transition
    {
        internal void SetTargetState(MovingState state)
        {
            base.SetTargetState(state);
        }
    }
}