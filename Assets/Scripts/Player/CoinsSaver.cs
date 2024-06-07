using Agava.YandexGames;
using System;

public class CoinsSaver
{
    private readonly Wallet _wallet;
    private readonly ConquestMonitor _monitor;
    private readonly int _points;
    private readonly float _speed;
    private readonly Claimer _player;
    private readonly PlayerData _playerData = new();

    public CoinsSaver(Wallet wallet, ConquestMonitor monitor, Claimer player)
    {
        _wallet = wallet != null ? wallet : throw new ArgumentNullException(nameof(wallet));
        _monitor = monitor != null ? monitor : throw new ArgumentNullException(nameof(monitor));
        _player = player != null ? player : throw new ArgumentNullException(nameof(player));

        _points = _playerData.Points;
        _speed = _playerData.Speed;

        _monitor.PlayerWon += OnPlayerWon;
        _monitor.BotWon += OnPlayerLose;
        _player.Died += OnPlayerLose;
    }

    ~CoinsSaver()
    {
        _monitor.PlayerWon -= OnPlayerWon;
        _monitor.BotWon -= OnPlayerLose;
        _player.Died -= OnPlayerLose;
    }

    private void OnPlayerLose()
    {
        SaveEarnedCoins();
    }

    private void OnPlayerWon(int part)
    {
        SaveEarnedCoins();
    }

    private void SaveEarnedCoins()
    {
        const string LeaderboardName = "Leaderboard1";

        int score = _points + _wallet.Coins;
        _playerData.Points = score;

        if (PlayerAccount.IsAuthorized == false)
            return;

        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result == null || result.score < score)
                Leaderboard.SetScore(LeaderboardName, score);
        });
    }
}
