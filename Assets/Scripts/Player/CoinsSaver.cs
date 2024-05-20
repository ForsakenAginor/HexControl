using System;

public class CoinsSaver
{
    private readonly Wallet _wallet;
    private readonly ConquestMonitor _monitor;
    private readonly int _points;
    private readonly float _speed;

    public CoinsSaver(Wallet wallet, ConquestMonitor monitor)
    {
        _wallet = wallet != null ? wallet : throw new ArgumentNullException(nameof(wallet));
        _monitor = monitor != null ? monitor : throw new ArgumentNullException(nameof(monitor));

        _points = PlayerData.Instance.Points;
        _speed = PlayerData.Instance.Speed;

        _monitor.PlayerWon += OnPlayerWon;
    }

    ~CoinsSaver()
    {
        _monitor.PlayerWon -= OnPlayerWon;
    }

    private void OnPlayerWon(int part)    {

        PlayerData.Instance.Save(_points + _wallet.Coins, _speed);
    }
}
