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
            PlayerAccount.RequestPersonalProfileDataPermission(OnSuccessCallback, OnErrorCallback);        
        else        
            _autorizationPanel.SetActive(true);        

        _holderPanel.SetActive(false);
    }

    private void OnErrorCallback(string _)
    {
        Fill();
    }

    private void OnSuccessCallback()
    {
        Fill();
    }

    private void Fill()
    {
        _leaderboard.SetPlayerScore(PlayerData.Instance.Points, Fill);
        _leaderboard.Fill();
        _leaderboardPanel.SetActive(true);
    }
}
