namespace Assets.Scripts.BotLogic.StateMachine.Transitions
{
    internal class ToFinalTransition : Transition
    {
        internal void SetTargetState(FinalState state)
        {
            base.SetTargetState(state);
        }
    }
}