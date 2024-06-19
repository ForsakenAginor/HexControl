using System;
using Assets.Scripts.BotLogic.StateMachine;
using Assets.Scripts.BotLogic.StateMachine.Transitions;
using Assets.Scripts.Core;
using Assets.Scripts.HexGrid;
using UnityEngine;

namespace Assets.Scripts.BotLogic
{
    [RequireComponent(typeof(Conquestor))]
    internal class Bot : MonoBehaviour
    {
        private State _state;
        private HexGridXZ<CellSprite> _grid;

        public event Action<State> StateChanged;
        public event Action<Vector2Int> Died;

        private void FixedUpdate()
        {
            _state.DoBotThing();

            foreach (Transition transition in _state.Transitions)
                if (transition.IsReadyToTransit)
                    SetState(transition);
        }

        internal void Init(HexGridXZ<CellSprite> grid, StartState startState)
        {
            _grid = grid != null ? grid : throw new ArgumentNullException(nameof(grid));
            _state = startState != null ? startState : throw new ArgumentNullException(nameof(startState));
        }

        private void SetState(Transition transition)
        {
            transition.SetIsReady(false);
            _state = transition.TargetState;
            StateChanged?.Invoke(_state);

            if (_state is DyingState)
                Died?.Invoke(_grid.GetXZ(transform.position));
        }
    }
}