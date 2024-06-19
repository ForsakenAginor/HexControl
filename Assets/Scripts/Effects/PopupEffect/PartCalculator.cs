using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Effects.PopupEffect
{
    public class PartCalculator
    {
        private readonly Claimer _claimer;
        private readonly int _gridSize;

        public PartCalculator(Claimer claimer, int gridSize)
        {
            _claimer = claimer != null ? claimer : throw new ArgumentNullException(nameof(claimer));
            _gridSize = gridSize > 0 ? gridSize : throw new ArgumentOutOfRangeException(nameof(gridSize));
            _claimer.CellsClimed += OnCellsClimed;
        }

        ~PartCalculator()
        {
            _claimer.CellsClimed -= OnCellsClimed;
        }

        public event Action<float> ScoreAdded;

        private void OnCellsClimed(IEnumerable<Vector2Int> cells)
        {
            float addedPercent = cells.Count() * 100f / _gridSize;
            ScoreAdded?.Invoke(addedPercent);
        }
    }
}