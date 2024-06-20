using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.HexGrid;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Conquestor : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private Color _color;

        private IEnumerable<Vector2Int> _claimedCells;
        private ClaimSystem _claimSystem;

        public event Action<IEnumerable<Vector2Int>> CellsClaimed;

        public int ClaimedCells => _claimedCells != null ? _claimedCells.Count() : 0;

        public string Name => _name;

        public Color Color => _color;

        public CellSprite CellSprite => _claimSystem.Color;

        private void OnDestroy()
        {
            _claimSystem.CellsClaimed -= OnCellsClaimed;
        }

        public void Init(ClaimSystem claimSystem)
        {
            _claimSystem = claimSystem != null ? claimSystem : throw new ArgumentNullException(nameof(claimSystem));
            _claimedCells = _claimSystem.ClaimedCells;

            _claimSystem.CellsClaimed += OnCellsClaimed;
        }

        private void OnCellsClaimed(IEnumerable<Vector2Int> cellsList)
        {
            CellsClaimed?.Invoke(cellsList);
        }
    }
}