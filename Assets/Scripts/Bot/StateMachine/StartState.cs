using System;
using System.Linq;
using UnityEngine;

namespace BotLogic
{
    internal class StartState : State
    {
        private readonly ClaimSystem _claimSystem;
        private readonly Transform _transform;
        private readonly HexGridXZ<CellSprite> _grid;

        internal StartState(Transform transform, ClaimSystem claimSystem, ToContestTransition transition) : base(new Transition[] { transition })
        {
            _claimSystem = claimSystem != null ? claimSystem : throw new ArgumentNullException(nameof(claimSystem));
            _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
            _grid = _claimSystem.Grid;
        }

        internal override void DoBotThing()
        {
            Vector3 position = new(_transform.position.x, _grid.OriginPosition.y, _transform.position.z);
            Vector2Int cellPosition = _grid.GetXZ(position);
            ClaimCell(cellPosition);
        }

        private void ClaimCell(Vector2Int cell)
        {
            _claimSystem.AddClaimedCell(cell);
            Transitions.First().SetIsReady(true);
        }
    }
}