using Agava.YandexGames;
using LeaderboardSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardOpener : MonoBehaviour
{
    [SerializeField] private YandexLeaderboard _leaderboard;
    [SerializeField] private GameObject _leaderboardPanel;
    [SerializeField] private Button _openLeaderboardButton;
    [SerializeField] private GameObject _holderPanel;

    private void OnEnable()
    {
        _openLeaderboardButton.onClick.AddListener(TryOpenLeaderboard);
    }

    private void OnDisable()
    {
        _openLeaderboardButton.onClick.RemoveListener(TryOpenLeaderboard);
    }

    private void TryOpenLeaderboard()
    {
        PlayerAccount.Authorize();

        if(PlayerAccount.IsAuthorized)        
            PlayerAccount.RequestPersonalProfileDataPermission(OnSuccessCallback);        
        else
            return;
    }

    private void OnSuccessCallback()
    {
        _leaderboard.SetPlayerScore(PlayerData.Instance.Points, FillCallback);
        _leaderboardPanel.SetActive(true);
        _holderPanel.SetActive(false);
    }

    private void FillCallback()
    {
        _leaderboard.Fill();
    }
}
