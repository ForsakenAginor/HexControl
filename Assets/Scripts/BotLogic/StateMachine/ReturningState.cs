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
    internal class ReturningState : State
    {
        private readonly ClaimSystem _claimSystem;
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly HexGridXZ<CellSprite> _grid;
        private readonly HexPathFinder _pathFinder;
        private readonly HexPathFinder _pathController;
        private readonly CellSprite _color;
        private readonly CellSprite _contestedColor;

        private bool _isActive = false;
        private Vector3 _curentTargetPoint;
        private Vector3 _startPoint;

        internal ReturningState(
            Transform transform, float speed, ClaimSystem claimSystem, ToDyingTransition toDyingTransition, ToMovingTransition toMovingTransition)
            : base(new Transition[] { toDyingTransition, toMovingTransition })
        {
            _claimSystem = claimSystem != null ? claimSystem : throw new ArgumentNullException(nameof(claimSystem));
            _transform = transform != null ? transform : throw new ArgumentNullException(nameof(transform));
            _grid = claimSystem.Grid;
            _speed = speed;
            _color = _claimSystem.Color;
            _contestedColor = _claimSystem.ContestedColor;
            _grid.GridObjectChanged += OnGridObjectChanged;

            _pathFinder = new HexPathFinder(_grid.Width, _grid.Height, _grid.CellSize);
            _pathController = new HexPathFinder(_grid.Width, _grid.Height, _grid.CellSize);

            for (int x = 0; x < _grid.Width; x++)
                for (int y = 0; y < _grid.Height; y++)
                    _pathController.MakeNodUnwalkable(new Vector2Int(x, y));
        }

        ~ReturningState()
        {
            _grid.GridObjectChanged -= OnGridObjectChanged;
        }

        internal override void DoBotThing()
        {
            if (_isActive == false)
            {
                _isActive = true;
                _curentTargetPoint = GetNextPoint(_grid.GetXZ(_transform.position));
                _startPoint = _grid.GetCellWorldPosition(_claimSystem.ClaimedCells.First());
            }

            if (_transform.position != _curentTargetPoint)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, _curentTargetPoint, _speed * Time.deltaTime);
                return;
            }

            Vector2Int cellPosition = _grid.GetXZ(_transform.position);
            ContestCell(cellPosition);

            _curentTargetPoint = GetNextPoint(cellPosition);
        }

        private void OnGridObjectChanged(Vector2Int cell)
        {
            _pathFinder.MakeNodWalkable(cell);
            CellSprite cellColor = _grid.GetGridObject(cell);

            foreach (CellSprite enemyColor in ColorAssignment.GetEnemyColors(_color))
                if (cellColor == enemyColor)
                    _pathFinder.MakeNodUnwalkable(cell);

            if (cellColor == _contestedColor || cellColor == _color)
                _pathController.MakeNodWalkable(cell);
            else
                _pathController.MakeNodUnwalkable(cell);
        }

        private Vector3 GetNextPoint(Vector2Int cellPosition)
        {
            if (_isActive == false)
                return default;

            Vector3 targetCoordinates = _grid.GetCellWorldPosition(_claimSystem.ClaimedCells.OrderBy(o => (o - cellPosition).sqrMagnitude).First());
            List<Vector3> path = _pathFinder.FindPath(new Vector3(_transform.position.x, _grid.OriginPosition.y, _transform.position.z), targetCoordinates).Skip(1).ToList();

            if (path.Count == 0)
            {
                _claimSystem.ClaimCells();
                _isActive = false;
                Transitions.First(o => o is ToMovingTransition).SetIsReady(true);
                return Vector3.zero;
            }

            return path.First();
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