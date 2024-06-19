using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.BotLogic;
using Assets.Scripts.HexGrid;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Core
{
    internal class CoinSpawner : MonoBehaviour
    {
        private readonly List<Vector3> _coinsPosition = new ();

        [SerializeField] private Coin _prefab;
        [SerializeField] private int _amount;

        private HexGridXZ<CellSprite> _grid;
        private Bot[] _bots;

        private void OnDestroy()
        {
            foreach (Bot bot in _bots)
                bot.Died -= OnBotDied;
        }

        public void Init(HexGridXZ<CellSprite> grid, Bot[] bots)
        {
            _bots = bots != null ? bots : throw new ArgumentNullException(nameof(bots));
            _grid = grid != null ? grid : throw new ArgumentNullException(nameof(grid));

            for (int i = 0; i < _amount; i++)
                SpawnCoin();

            foreach (Bot bot in _bots)
                bot.Died += OnBotDied;
        }

        private void OnBotDied(Vector2Int cell)
        {
            Vector3 coinHeightFactor = new (0, 0.6f, 0);
            IEnumerable<Vector3> coordinatesForCoins = _grid.CashedNeighbours[cell]
                .Where(o => _grid.IsValidGridPosition(o))
                .Select(o => _grid.GetCellWorldPosition(o) + coinHeightFactor);
            Vector3 spawnPoint = _grid.GetCellWorldPosition(cell) + coinHeightFactor;
            Quaternion quaternion = new (180, 0, 0, 0);
            float duration = 1f;
            float height = 1f;
            int animationCount = 2;
            Coin coin;

            foreach (Vector3 coordinate in coordinatesForCoins)
            {
                coin = Instantiate(_prefab, spawnPoint, quaternion);
                coin.transform.DOMove(coordinate, duration).SetEase(Ease.Linear);
                coin.transform.DOMoveY(coordinate.y + height, duration / animationCount).SetLoops(animationCount, LoopType.Yoyo);
            }
        }

        private void SpawnCoin()
        {
            bool isSpawned = false;
            Quaternion quaternion = new (180, 0, 0, 0);
            Vector3 coinHeightFactor = new (0, 0.6f, 0);

            while (isSpawned == false)
            {
                int xPosition = UnityEngine.Random.Range(0, _grid.Width);
                int yPosition = UnityEngine.Random.Range(0, _grid.Height);
                Vector3 position = _grid.GetCellWorldPosition(xPosition, yPosition) + coinHeightFactor;

                if (_coinsPosition.Contains(position) == false)
                {
                    Instantiate(_prefab, position, quaternion);
                    isSpawned = true;
                    _coinsPosition.Add(position);
                }
            }
        }
    }
}