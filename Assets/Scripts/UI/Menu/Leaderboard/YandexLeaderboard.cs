using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

namespace LeaderboardSystem
{
    public class YandexLeaderboard : MonoBehaviour
    {
        private const string LeaderboardName = "Leaderboard";
        private const string AnonymousName = "Anonymous";

        [SerializeField] private LeaderboardView _leaderboardView;

        private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

        public void SetPlayerScore(int score)
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
            {
                if (result == null || result.score < score)
                    Leaderboard.SetScore(LeaderboardName, score);
            });
        }

        public void Fill()
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            _leaderboardPlayers.Clear();

            Leaderboard.GetEntries(LeaderboardName, (result) =>
            {
                foreach (var entry in result.entries)
                {
                    int rank = entry.rank;
                    int score = entry.score;
                    string name = entry.player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = AnonymousName;

                    _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
                }

                _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
            });
        }
    }
}
