using TMPro;
using UnityEngine;

public class PlayerDataView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speedTextLabel;
    [SerializeField] private TextMeshProUGUI _pointsTextLavel;

    private void Start()
    {
        OnPlayerSpeedBoosted();
    }

    private void OnEnable()
    {
        PlayerData.Instance.SpeedBoosted += OnPlayerSpeedBoosted;
    }

    private void OnDisable()
    {
        PlayerData.Instance.SpeedBoosted -= OnPlayerSpeedBoosted;        
    }

    private void OnPlayerSpeedBoosted()
    {
        _speedTextLabel.text = PlayerData.Instance.Speed.ToString();
        _pointsTextLavel.text = PlayerData.Instance.Points.ToString();
    }
}
