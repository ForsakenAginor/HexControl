using System;
using Assets.Scripts.BotLogic.StateMachine.Transitions;

namespace Assets.Scripts.BotLogic.StateMachine
{
    internal abstract class State
    {
        private readonly Transition[] _transitions;

        internal State(Transition[] transitions)
        {
            _transitions = transitions != null ? transitions : throw new ArgumentNullException(nameof(transitions));
        }

        internal Transition[] Transitions => _transitions;

        internal abstract void DoBotThing();
    }
}