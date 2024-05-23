using HexPathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Conquestor))]
public class Claimer : MonoBehaviour
{
    private CellSprite _color;
    private CellSprite _contestedColor;
    private ClaimSystem _claimSystem;
    private HexGridXZ<CellSprite> _grid;
    private Transform _transform;
    private HexPathFinder _pathController;
    private Vector3 _startPoint;
    private bool _isDead;

    public event Action Died;
    public event Action CellsClimed;

    private void FixedUpdate()
    {
        if (_isDead)
        {
            _claimSystem.Die();
            Died?.Invoke();
            enabled = false;
            return;
        }

        Vector2Int cell = _grid.GetXZ(_transform.position);
        CellSprite color = _grid.GetGridObject(cell);

        if (_claimSystem.CanContest(cell))
        {
            _grid.SetGridObject(cell.x, cell.y, _contestedColor);
            _claimSystem.AddContestedCell(cell);
            return;
        }

        if (color == _color)
        {
            _claimSystem.ClaimCells();
        }
    }

    private void OnDestroy()
    {
        _grid.GridObjectChanged -= OnGridObjectChanged;
        _claimSystem.CellsClaimed -= OnCellsClaimed;
    }

    public void Init(HexGridXZ<CellSprite> grid, CellSprite color, CellSprite contestedColor)
    {
        _grid = grid != null ? grid : throw new ArgumentNullException(nameof(grid));
        _color = color;
        _contestedColor = contestedColor;
        _claimSystem = new(_grid, _color, _contestedColor);
        GetComponent<Conquestor>().Init(_claimSystem);
        _transform = transform;
        _startPoint = transform.position;
        _pathController = new HexPathFinder(_grid.Width, _grid.Height, _grid.CellSize);

        for (int x = 0; x < _grid.Width; x++)
            for (int y = 0; y < _grid.Height; y++)
                _pathController.MakeNodUnwalkable(new Vector2Int(x, y));

        _grid.GridObjectChanged += OnGridObjectChanged;
        _claimSystem.AddClaimedCell(_grid.GetXZ(_transform.position));
        _claimSystem.CellsClaimed += OnCellsClaimed;
    }

    private void OnCellsClaimed(IEnumerable<Vector2Int> cells)
    {
        CellsClimed?.Invoke();
    }

    private void OnGridObjectChanged(Vector2Int cell)
    {
        Vector2Int currentCell = _grid.GetXZ(_transform.position);
        _pathController.MakeNodWalkable(currentCell);

        CellSprite cellColor = _grid.GetGridObject(cell);

        if (cellColor == _contestedColor || cellColor == _color)
            _pathController.MakeNodWalkable(cell);
        else
            _pathController.MakeNodUnwalkable(cell);

        if (_pathController.FindPath(_transform.position, _startPoint) == null)
            _isDead = true;

        _pathController.MakeNodUnwalkable(currentCell);
    }
}