using System;

namespace BotLogic
{
    internal class DyingState : State
    {
        private readonly ClaimSystem _claimSystem;
        private bool _isDead = false;

        internal DyingState(ClaimSystem claimSystem) : base(Array.Empty<Transition>())
        {
            _claimSystem = claimSystem != null ? claimSystem : throw new ArgumentNullException(nameof(claimSystem));
        }

        internal override void DoBotThing()
        {
            if (_isDead == false)
            {
                _claimSystem.Die();
                _isDead = true;
            }

            return;
        }
    }
}