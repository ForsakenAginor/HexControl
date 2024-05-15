using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreField;

    public void Init(ScoreCalculator scoreCalculator)
    {
        _scoreField.text = scoreCalculator.Score.ToString();
    }
}
