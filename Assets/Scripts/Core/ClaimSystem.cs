using HexPathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClaimSystem
{
    private readonly List<Vector2Int> _claimedCells = new();
    private readonly IReadOnlyDictionary<Vector2Int, IEnumerable<Vector2Int>> _cashedNeighbours;
    private readonly IEnumerable<CellSprite> _enemiesColors;
    private readonly CellSprite _color;
    private readonly CellSprite _contestedColor;
    private readonly HexGridXZ<CellSprite> _grid;
    private readonly HexPathFinder _pathFinder;

    private List<Vector2Int> _contestedCells = new();

    public ClaimSystem(HexGridXZ<CellSprite> grid, CellSprite color, CellSprite contestedColor)
    {
        _grid = grid != null ? grid : throw new ArgumentNullException(nameof(grid));
        _color = color;
        _contestedColor = contestedColor;
        _cashedNeighbours = _grid.CashedNeighbours;
        _pathFinder = new HexPathFinder(_grid.Width, _grid.Height, _grid.CellSize);
        _enemiesColors = ColorAssignment.GetEnemyColors(_color);
    }

    public event Action<IEnumerable<Vector2Int>> CellsClaimed;

    public CellSprite Color => _color;
    public CellSprite ContestedColor => _contestedColor;
    public IEnumerable<Vector2Int> ClaimedCells => _claimedCells;
    public IEnumerable<Vector2Int> ContestedCells => _contestedCells;
    public IReadOnlyDictionary<Vector2Int, IEnumerable<Vector2Int>> CashedNeighbours => _cashedNeighbours;
    public IEnumerable<CellSprite> EnemiesColors => _enemiesColors;
    public HexGridXZ<CellSprite> Grid => _grid;

    public void AddClaimedCell(Vector2Int cell)
    {
        CellSprite cellSprite = _grid.GetGridObject(cell.x, cell.y);

        if (_grid.IsValidGridPosition(cell) == false || cellSprite != CellSprite.Empty)
            throw new Exception("WrongSpawnPoint");

        _grid.SetGridObject(cell.x, cell.y, _color);

        if (_claimedCells.Contains(cell) == false)
            _claimedCells.Add(cell);
    }

    public void AddContestedCell(Vector2Int cell)
    {
        if (_grid.IsValidGridPosition(cell))
            _contestedCells.Add(cell);
    }

    public bool CanContest(Vector2Int cell)
    {
        CellSprite color = _grid.GetGridObject(cell);

        foreach (CellSprite enemyColor in _enemiesColors)
            if (color == enemyColor)
                return false;

        if (color == _color || color == _contestedColor)
            return false;

        return true;
    }

    public void ClaimCells()
    {
        if (_claimedCells.Count == 0 || _contestedCells.Count == 0)
            return;

        IEnumerable<Vector2Int> circledCells = GetCircledCells();
        ResetPathFinder();
        List<Vector2Int> temp = new();
        List<Vector2Int> claimedCells = new();

        foreach (var cell in _claimedCells)
            _pathFinder.MakeNodWalkable(cell);

        foreach (var cell in ContestedCells)
        {
            if (_grid.GetGridObject(cell) == _contestedColor)
            {
                _pathFinder.MakeNodWalkable(cell);
                temp.Add(cell);
            }
        }

        _contestedCells = temp;

        foreach (var cell in ContestedCells)
        {
            if (_grid.IsValidGridPosition(cell) && _claimedCells.Contains(cell) == false)
            {
                if (_pathFinder.FindPath(_grid.GetCellWorldPosition(_claimedCells.First()), _grid.GetCellWorldPosition(cell)) == null)
                {
                    _grid.SetGridObject(cell.x, cell.y, CellSprite.Empty);
                }
                else
                {
                    _claimedCells.Add(cell);
                    claimedCells.Add(cell);
                    _grid.SetGridObject(cell.x, cell.y, _color);
                }
            }
        }

        foreach (var cell in circledCells)
        {
            if (_claimedCells.Contains(cell) == false)
            {
                _claimedCells.Add(cell);
                claimedCells.Add(cell);
                _grid.SetGridObject(cell.x, cell.y, _color);
            }
        }

        CellsClaimed?.Invoke(claimedCells);
        _contestedCells.Clear();
    }

    public void Die()
    {
        var cells = _contestedCells.Union(_claimedCells);

        foreach (var cell in cells)
        {
            CellSprite cellColor = _grid.GetGridObject(cell);

            if (cellColor == _contestedColor || cellColor == _color)
                _grid.SetGridObject(cell.x, cell.y, CellSprite.Empty);
        }

        _claimedCells.Clear();
        _contestedCells.Clear();
    }

    private IEnumerable<Vector2Int> GetCircledCells()
    {
        ResetPathFinder();

        if (TryFindBorder(out List<Vector2Int> border) == false)
            return Array.Empty<Vector2Int>();

        return GetCellsInsideBorder(border);
    }

    private bool TryFindBorder(out List<Vector2Int> border)
    {
        List<Vector2Int> contested = _contestedCells.Where(o => _grid.GetGridObject(o) == _contestedColor).ToList();
        border = new();

        for (int i = 0; i < contested.Count; i++)
        {
            if (border.Count == 0)
            {
                if (i + 1 < contested.Count && _cashedNeighbours[contested[i + 1]].Intersect(_claimedCells).Count() == 0)
                {
                    border.Add(contested[i]);
                    _pathFinder.MakeNodWalkable(contested[i]);
                }
            }
            else
            {
                border.Add(contested[i]);
                _pathFinder.MakeNodWalkable(contested[i]);
            }
        }

        if (border.Count == 0)
            return false;

        border = _pathFinder.FindPath(border.First(), border.Last());

        if (border == null)
            return false;

        ResetPathFinder();
        _pathFinder.MakeNodWalkable(border.First());
        _pathFinder.MakeNodWalkable(border.Last());

        foreach (var cell in _claimedCells)
            _pathFinder.MakeNodWalkable(cell);

        var path = _pathFinder.FindPath(border.First(), border.Last());

        if (path == null || path.Count == 0)
            return false;

        border.AddRange(path.Skip(1).SkipLast(1));
        return true;
    }

    private IEnumerable<Vector2Int> GetCellsInsideBorder(IEnumerable<Vector2Int> border)
    {
        var ys = border.Select(o => o.y);
        var xs = border.Select(o => o.x);
        int maxY = ys.Max();
        int minY = ys.Min();
        int maxX = xs.Max();
        int minX = xs.Min();
        bool isOdd = minY % 2 == 1;
        ResetPathFinder();

        for (int i = minX + 1; i < maxX; i++)
            for (int j = minY + 1; j < maxY; j++)
                _pathFinder.MakeNodWalkable(new Vector2Int(i, j));

        foreach (var cell in border)
            _pathFinder.MakeNodUnwalkable(cell);

        List<Vector2Int> insideCells = new();
        Vector2Int targetCell;
        Vector2Int bottomBorderCell = border.Where(o => o.y == minY).OrderBy(o => o.x).First();

        if (isOdd == false)
            targetCell = new Vector2Int(bottomBorderCell.x, bottomBorderCell.y + 1);
        else
            targetCell = new Vector2Int(bottomBorderCell.x + 1, bottomBorderCell.y + 1);

        for (int i = minX + 1; i < maxX; i++)
        {
            for (int j = minY + 1; j < maxY; j++)
            {
                Vector2Int cell = new(i, j);

                if (border.Contains(cell) == false && _pathFinder.FindPath(cell, targetCell) != null)
                    insideCells.Add(cell);
            }
        }

        return insideCells;
    }

    private void ResetPathFinder()
    {
        for (int x = 0; x < _grid.Width; x++)
            for (int y = 0; y < _grid.Height; y++)
                _pathFinder.MakeNodUnwalkable(new Vector2Int(x, y));
    }
}