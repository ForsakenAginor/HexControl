using System;
using Assets.Scripts.BotLogic.StateMachine.Transitions;

namespace Assets.Scripts.BotLogic.StateMachine
{
    internal class FinalState : State
    {
        internal FinalState()
            : base(Array.Empty<Transition>())
        {
        }

        internal override void DoBotThing()
        {
            return;
        }
    }
}