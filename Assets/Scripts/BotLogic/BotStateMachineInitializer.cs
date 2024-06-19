using System;
using Assets.Scripts.BotLogic.StateMachine;
using Assets.Scripts.BotLogic.StateMachine.Transitions;
using Assets.Scripts.Core;
using Assets.Scripts.HexGrid;
using UnityEngine;

namespace Assets.Scripts.BotLogic
{
    [RequireComponent(typeof(Conquestor))]
    [RequireComponent(typeof(Bot))]
    public class BotStateMachineInitializer : MonoBehaviour
    {
        [SerializeField] private CellSprite _color;
        [SerializeField] private CellSprite _contestedColor;
        [SerializeField] private float _speed;
        [SerializeField] private int _contestLength;

        public void Init(HexGridXZ<CellSprite> grid)
        {
            grid = grid != null ? grid : throw new ArgumentNullException(nameof(grid));
            ClaimSystem claimSystem = new (grid, _color, _contestedColor);
            GetComponent<Conquestor>().Init(claimSystem);

            ToContestTransition toContestTransition = new ();
            ToDyingTransition toDyingTransition = new ();
            ToReturningTransition toReturningTransition = new ();
            ToMovingTransition toMovingTransition = new ();
            ToFinalTransition toFinalTransition = new ();

            StartState startState = new (transform, claimSystem, toContestTransition);
            ContestState contestState = new (_contestLength, transform, _speed, claimSystem, toDyingTransition, toReturningTransition);
            ReturningState returningState = new (transform, _speed, claimSystem, toDyingTransition, toMovingTransition);
            MovingState movingState = new (transform, _speed, claimSystem, toContestTransition, toFinalTransition);
            FinalState finalState = new ();
            DyingState dyingState = new (claimSystem);

            toContestTransition.SetTargetState(contestState);
            toDyingTransition.SetTargetState(dyingState);
            toReturningTransition.SetTargetState(returningState);
            toMovingTransition.SetTargetState(movingState);
            toFinalTransition.SetTargetState(finalState);

            GetComponent<Bot>().Init(grid, startState);
        }
    }
}