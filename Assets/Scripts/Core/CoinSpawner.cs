using System;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private int _amount;

    private HexGridXZ<CellSprite> _grid;
    private List<Vector3> _coinsPosition = new();

    public void Init(HexGridXZ<CellSprite> grid)
    {
        _grid = grid != null ? grid : throw new ArgumentNullException(nameof(grid));

        for (int i = 0; i < _amount; i++)
            SpawnCoin();
    }

    private void SpawnCoin()
    {
        bool isSpawned = false;
        Quaternion quaternion = new(180, 0, 0, 0);
        Vector3 coinHeightFactor = new Vector3(0, 0.6f, 0);

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
