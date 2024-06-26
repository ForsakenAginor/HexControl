using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.HexGrid;
using Assets.Scripts.Player;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class ConquestMonitor : MonoBehaviour
    {
        private readonly Dictionary<Conquestor, ConquestBar> _conquestors = new ();

        [SerializeField] private ConquestBar _prefab;
        [SerializeField] private RectTransform _holder;

        private HexGridXZ<CellSprite> _grid;
        private int _hexes;

        public event Action BotWon;

        public event Action<int> PlayerWon;

        private void OnDestroy()
        {
            _grid.GridObjectChanged -= OnGridObjectChanged;
        }

        public void Init(HexGridXZ<CellSprite> grid)
        {
            _grid = grid != null ? grid : throw new ArgumentNullException(nameof(grid));
            _hexes = grid.Width * grid.Height;
            _grid.GridObjectChanged += OnGridObjectChanged;
        }

        public void AddConquestor(Conquestor conquestor)
        {
            if (conquestor == null)
                throw new ArgumentNullException(nameof(conquestor));

            ConquestBar bar = Instantiate(_prefab, _holder);
            bar.ChangeColor(conquestor.Color);
            UpdateBar(conquestor, bar);
            _conquestors.Add(conquestor, bar);
        }

        private void UpdateBar(Conquestor conquestor, ConquestBar bar)
        {
            float part = (float)conquestor.ClaimedCells / _hexes;
            bar.ChangeSliderValue(part);
            bar.ChangeText(conquestor.Name, part);
        }

        private void OnGridObjectChanged(Vector2Int nonmatterValue)
        {
            int totalClaimedHexes = 0;

            foreach (var conquestor in _conquestors.Keys)
            {
                totalClaimedHexes += conquestor.ClaimedCells;
                UpdateBar(conquestor, _conquestors[conquestor]);
            }

            var conquestors = _conquestors.Keys.OrderByDescending(o => o.ClaimedCells).ToList();

            for (int i = 0; i < conquestors.Count; i++)
                _conquestors[conquestors[i]].transform.SetSiblingIndex(i);

            if (totalClaimedHexes == _hexes)
                FindWinner();
        }

        private void FindWinner()
        {
            var conquestor = _conquestors.Keys.OrderByDescending(o => o.ClaimedCells).First();
            int part = conquestor.ClaimedCells * 100 / _hexes;
            var winner = conquestor.GetComponentInChildren<IWinner>();
            winner.BecomeWinner();

            if (winner is PlayerAnimationHandler)
                PlayerWon?.Invoke(part);
            else
                BotWon?.Invoke();
        }
    }
}