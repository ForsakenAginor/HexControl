using System;

public class ScoreCalculator
{
    private Wallet _wallet;
    private ConquestMonitor _monitor;

    public ScoreCalculator(Wallet wallet, ConquestMonitor monitor)
    {
        _wallet = wallet != null ? wallet : throw new ArgumentNullException(nameof(wallet));
        _monitor = monitor != null ? monitor : throw new ArgumentNullException(nameof(monitor));

        _monitor.PlayerWon += OnPlayerWon;
    }

    ~ScoreCalculator()
    {
        _monitor.PlayerWon -= OnPlayerWon;
    }

    public int Score { get; private set; }

    private void OnPlayerWon(int part)
    {
        Score = part * _wallet.Coins;
        PlayerData.Instance.Add(Score);
    }
}
