using System;

namespace Assets.Scripts.BotLogic.StateMachine.Transitions
{
    internal abstract class Transition
    {
        private State _targetState;
        private bool _isReadyToTransit;

        internal State TargetState => _targetState;

        internal bool IsReadyToTransit => _isReadyToTransit;

        internal void SetIsReady(bool value)
        {
            _isReadyToTransit = value;
        }

        protected void SetTargetState(State state)
        {
            _targetState = state != null ? state : throw new ArgumentNullException(nameof(state));
        }
    }
}