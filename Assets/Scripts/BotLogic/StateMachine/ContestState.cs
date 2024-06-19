using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.BotLogic.StateMachine.Transitions;
using Assets.Scripts.Core;
using Assets.Scripts.HexGrid;
using HexPathfinding;
using UnityEngine;

namespace Assets.Scripts.BotLogic.StateMachine
{
    internal class ContestState : State
    {
        private readonly ClaimSystem _claimSystem;
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly HexGridXZ<CellSprite> _grid;
        private readonly IReadOnlyDictionary<Vector2Int, IEnumerable<Vector2Int>> _cashedNeighbours;
        private readonly int _contestSize;
        private readonly CellSprite _color;
        private readonly CellSprite _contestedColor;
        private readonly HexPathFinder _pathController;

        private bool _isActive = false;
        private int _contestedCellsCount;
        private Vector3 _startPoint;
        private Vector3 _curentTargetPoint;

        internal ContestState(
            int contestLenth, Transform transform, float speed, ClaimSystem claimSystem, ToDyingTransition toDyingTransition, ToReturningTransition toReturningTransition)
            : base(new Transition[] { toDyingTransition, toReturningTransition })
        {
            _claimSystem = claimSystem != null ? claimSystem : throw new ArgumentNullException(nameof(claimSystem));
            _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
            _contestSize = contestLenth;
            _grid = claimSystem.Grid;
            _speed = speed;
            _cashedNeighbours = _claimSystem.CashedNeighbours;
            _color = _claimSystem.Color;
            _contestedColor = _claimSystem.ContestedColor;

            _pathController = new HexPathFinder(_grid.Width, _grid.Height, _grid.CellSize);

            for (int x = 0; x < _grid.Width; x++)
                for (int y = 0; y < _grid.Height; y++)
                    _pathController.MakeNodUnwalkable(new Vector2Int(x, y));

            _grid.GridObjectChanged += OnGridObjectChanged;
        }

        ~ContestState()
        {
            _grid.GridObjectChanged -= OnGridObjectChanged;
        }

        internal override void DoBotThing()
        {
            if (_isActive == false)
            {
                _isActive = true;
                _contestedCellsCount = 0;
                _startPoint = _grid.GetCellWorldPosition(_claimSystem.ClaimedCells.First());
                _curentTargetPoint = _transform.position;
            }

            if (_transform.position != _curentTargetPoint)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _curentTargetPoint, _speed * Time.deltaTime);
                return;
            }

            Vector2Int cellPosition = _grid.GetXZ(_transform.position);
            ContestCell(cellPosition);

            if (_contestedCellsCount >= _contestSize)
            {
                _isActive = false;
                Transitions.First(o => o is ToReturningTransition).SetIsReady(true);
            }

            _curentTargetPoint = GetNextPoint(cellPosition);
        }

        private void OnGridObjectChanged(Vector2Int cell)
        {
            CellSprite cellColor = _grid.GetGridObject(cell);

            if (cellColor == _contestedColor || cellColor == _color)
                _pathController.MakeNodWalkable(cell);
            else
                _pathController.MakeNodUnwalkable(cell);
        }

        private Vector3 GetNextPoint(Vector2Int cellPosition)
        {
            if (_isActive == false)
                return default;

            List<Vector2Int> neighbours = _cashedNeighbours[cellPosition].Where(o => _grid.IsValidGridPosition(o.x, o.y)).ToList();
            List<Vector2Int> unClaimedNeigbours = neighbours.Where(o => _claimSystem.CanContest(o)).ToList();
            Vector2Int targetCellPosition;

            if (unClaimedNeigbours.Count == 0)
            {
                _isActive = false;
                Transitions.First(o => o is ToReturningTransition).SetIsReady(true);
                return default;
            }

            targetCellPosition = unClaimedNeigbours[UnityEngine.Random.Range(0, unClaimedNeigbours.Count)];
            Vector3 targetCoordinates = _grid.GetCellWorldPosition(targetCellPosition.x, targetCellPosition.y);
            targetCoordinates.y = _transform.position.y;

            return targetCoordinates;
        }

        private void ContestCell(Vector2Int cell)
        {
            if (_grid.IsValidGridPosition(cell.x, cell.y) == false)
                return;

            _pathController.MakeNodWalkable(cell);

            if (_pathController.FindPath(_transform.position, _startPoint) == null)
            {
                _isActive = false;
                Transitions.First(o => o is ToDyingTransition).SetIsReady(true);
                return;
            }

            if (_claimSystem.CanContest(cell))
            {
                _grid.SetGridObject(cell.x, cell.y, _contestedColor);
                _claimSystem.AddContestedCell(cell);
                _contestedCellsCount++;
                return;
            }

            CellSprite cellColor = _grid.GetGridObject(cell.x, cell.y);

            foreach (CellSprite enemyColor in ColorAssignment.GetEnemyColors(_color))
            {
                if (cellColor == enemyColor)
                {
                    _isActive = false;
                    Transitions.First(o => o is ToDyingTransition).SetIsReady(true);
                }
            }
        }
    }
}