using System;
using UnityEngine;

namespace BotLogic
{
    [RequireComponent(typeof(Conquestor))]
    internal class Bot : MonoBehaviour
    {
        [SerializeField] private CellSprite _color;
        [SerializeField] private CellSprite _contestedColor;
        [SerializeField] private float _speed;
        [SerializeField] private int _contestLength;

        private ClaimSystem _claimSystem;
        private State _state;
        private HexGridXZ<CellSprite> _grid;

        public event Action<State> StateChanged;

        private void FixedUpdate()
        {
            _state.DoBotThing();

            foreach (Transition transition in _state.Transitions)
                if (transition.IsReadyToTransit)
                    SetState(transition);
        }

        internal void Init(HexGridXZ<CellSprite> grid)
        {
            _grid = grid != null ? grid : throw new ArgumentNullException(nameof(grid));
            _claimSystem = new ClaimSystem(_grid, _color, _contestedColor);
            GetComponent<Conquestor>().Init(_claimSystem);
            InitStateMachine();
        }

        private void SetState(Transition transition)
        {
            transition.SetIsReady(false);
            _state = transition.TargetState;
            StateChanged?.Invoke(_state);
        }

        private void InitStateMachine()
        {
            ToContestTransition toContestTransition = new();
            ToDyingTransition toDyingTransition = new();
            ToReturningTransition toReturningTransition = new();
            ToMovingTransition toMovingTransition = new();
            ToFinalTransition toFinalTransition = new();

            StartState startState = new(transform, _claimSystem, toContestTransition);
            ContestState contestState = new(_contestLength, transform, _speed, _claimSystem, toDyingTransition, toReturningTransition);
            ReturningState returningState = new(transform, _speed, _claimSystem, toDyingTransition, toMovingTransition);
            MovingState movingState = new(transform, _speed, _claimSystem, toContestTransition, toFinalTransition);
            FinalState finalState = new();
            DyingState dyingState = new(_claimSystem);

            toContestTransition.SetTargetState(contestState);
            toDyingTransition.SetTargetState(dyingState);
            toReturningTransition.SetTargetState(returningState);
            toMovingTransition.SetTargetState(movingState);
            toFinalTransition.SetTargetState(finalState);

            _state = startState;
        }
    }
}