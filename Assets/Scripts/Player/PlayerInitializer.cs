using System;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Claimer _claimer;
    [SerializeField] private CoinPickuper _coinPickuper;
    [SerializeField] private PlayerAnimationHandler _playerAnimationHandler;
    [SerializeField] private CellSprite _color;
    [SerializeField] private CellSprite _contestedColor;

    public void Init(HexGridXZ<CellSprite> grid, Wallet wallet)
    {
        if(wallet == null)
            throw new ArgumentNullException(nameof(wallet));

        if(grid == null)
            throw new ArgumentNullException(nameof(grid));

        PlayerData playerData = new();
        float speed = playerData.Speed;
        _mover.Init(_playerAnimationHandler.transform, grid, _color, speed);
        _claimer.Init(grid, _color, _contestedColor);
        _coinPickuper.Init(wallet);
        _playerAnimationHandler.Init(_claimer, _mover);
    }
}
