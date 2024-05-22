using UnityEngine;

namespace LeaderboardSystem
{
    internal class LeaderboardElement : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _nameField;
        [SerializeField] private TMPro.TextMeshProUGUI _rankField;
        [SerializeField] private TMPro.TextMeshProUGUI _scoreField;

        internal void Init(string name, int rank, int score)
        {
            _nameField.text = name;
            _rankField.text = rank.ToString();
            _scoreField.text = score.ToString();
        }
    }
}
