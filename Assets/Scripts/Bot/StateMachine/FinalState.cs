using System;

namespace BotLogic
{
    internal class FinalState : State
    {
        internal FinalState() : base(Array.Empty<Transition>())
        {
        }

        internal override void DoBotThing()
        {
            return;
        }
    }
}