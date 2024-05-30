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
    [SerializeField] private GameObject _autorizationPanel;

    private void OnEnable()
    {
        _openLeaderboardButton.onClick.AddListener(TryOpenLeaderboard);
    }

    private void OnDisable()
    {
        _openLeaderboardButton.onClick.RemoveListener(TryOpenLeaderboard);
    }

    public void ShowLeaderboard()
    {
        if (PlayerAccount.IsAuthorized == false)
            throw new Exception("You trying to show leaderboard, when player is not autorized");

        PlayerAccount.RequestPersonalProfileDataPermission(OnSuccessCallback);
    }

    private void TryOpenLeaderboard()
    {
        bool isAuthorized;
#if UNITY_EDITOR
        isAuthorized = false;
#else   
        isAuthorized = PlayerAccount.IsAuthorized;
#endif
        if (isAuthorized)
            PlayerAccount.RequestPersonalProfileDataPermission(OnSuccessCallback);
        else
            _autorizationPanel.SetActive(true);

        _holderPanel.SetActive(false);
    }

    private void OnSuccessCallback()
    {
        _leaderboard.SetPlayerScore(PlayerData.Instance.Points, FillCallback);
        _leaderboardPanel.SetActive(true);
    }

    private void FillCallback()
    {
        _leaderboard.Fill();
    }
}
