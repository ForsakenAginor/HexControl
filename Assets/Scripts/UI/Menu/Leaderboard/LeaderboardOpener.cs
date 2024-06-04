using Agava.YandexGames;
using LeaderboardSystem;
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

    public void TryOpenLeaderboard()
    {
        bool isAuthorized;
#if UNITY_EDITOR
        isAuthorized = false;
#else   
        isAuthorized = PlayerAccount.IsAuthorized;
#endif
        if (isAuthorized)
            PlayerAccount.RequestPersonalProfileDataPermission();
        else
            _autorizationPanel.SetActive(true);

        _holderPanel.SetActive(false);
        _leaderboard.Fill();
        _leaderboardPanel.SetActive(true);
    }
}
