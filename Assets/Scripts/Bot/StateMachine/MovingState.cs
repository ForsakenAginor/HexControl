using HexPathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BotLogic
{
    internal class MovingState : State
    {
        private readonly ClaimSystem _claimSystem;
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly IReadOnlyDictionary<Vector2Int, IEnumerable<Vector2Int>> _cashedNeighbours;
        private readonly HexGridXZ<CellSprite> _grid;
        private readonly HexPathFinder _pathFinder;
        private readonly CellSprite _color;

        private bool _isActive = false;
        private Vector3 _curentTargetPoint;
        private Queue<Vector3> _path = new();

        internal MovingState(Transform transform, float speed, ClaimSystem claimSystem, ToContestTransition toContestTransition, ToFinalTransition toFinalTransition)
            : base(new Transition[] { toContestTransition, toFinalTransition })
        {
            _claimSystem = claimSystem != null ? claimSystem : throw new ArgumentNullException(nameof(claimSystem));
            _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
            _grid = claimSystem.Grid;
            _speed = speed;
            _cashedNeighbours = _claimSystem.CashedNeighbours;
            _color = _claimSystem.Color;
            _grid.GridObjectChanged += OnGridObjectChanged;

            _pathFinder = new HexPathFinder(_grid.Width, _grid.Height, _grid.CellSize);
        }

        ~MovingState()
        {
            _grid.GridObjectChanged -= OnGridObjectChanged;
        }

        internal override void DoBotThing()
        {
            if (_isActive == false)
                _curentTargetPoint = GetNextPoint();

            if (_transform.position != _curentTargetPoint)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _curentTargetPoint, _speed * Time.deltaTime);
                return;
            }

            _curentTargetPoint = GetNextPoint();
        }

        private void OnGridObjectChanged(Vector2Int cell)
        {
            _pathFinder.MakeNodUnwalkable(cell);
            CellSprite cellColor = _grid.GetGridObject(cell);

            if (cellColor == CellSprite.Empty || cellColor == _color)
                _pathFinder.MakeNodWalkable(cell);
        }


        private Vector3 GetNextPoint()
        {
            if (_path.TryDequeue(out Vector3 result))
            {
                return result;
            }
            else if (_isActive)
            {
                _isActive = false;
                Transitions.First(o => o is ToContestTransition).SetIsReady(true);
                return result;
            }

            _isActive = true;
            Vector3 targetCoordinates = _grid.GetCellWorldPosition(FindUnclaimedCell(_grid.GetXZ(_transform.position)));

            if (_isActive)
            {
                List<Vector3> path = _pathFinder.FindPath(_transform.position, targetCoordinates).ToList();

                foreach (Vector3 point in path)
                    _path.Enqueue(point);

                targetCoordinates = _path.Dequeue();
            }

            return targetCoordinates;
        }

        private Vector2Int FindUnclaimedCell(Vector2Int currentPosition)
        {
            foreach (Vector2Int claimedCell in _claimSystem.ClaimedCells.OrderBy(o => (o - currentPosition).sqrMagnitude))
            {
                IEnumerable<Vector2Int> unclaimedCells = _cashedNeighbours[claimedCell].Where(o => _grid.IsValidGridPosition(o.x, o.y) && _grid.GetGridObject(o.x, o.y) == CellSprite.Empty);

                if (unclaimedCells.Count() > 0)
                    return claimedCell;
            }

            _isActive = false;
            Transitions.First(o => o is ToFinalTransition).SetIsReady(true);
            return currentPosition;
        }
    }
}