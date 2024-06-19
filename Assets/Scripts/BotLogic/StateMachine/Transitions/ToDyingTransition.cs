namespace Assets.Scripts.BotLogic.StateMachine.Transitions
{
    internal class ToDyingTransition : Transition
    {
        internal void SetTargetState(DyingState state)
        {
            base.SetTargetState(state);
        }
    }
}