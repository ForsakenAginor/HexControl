using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Menu.Profile
{
    public class PlayerDataView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _speedTextLabel;
        [SerializeField] private TextMeshProUGUI _pointsTextLavel;

        private PlayerDataChanger _playerData;

        private void OnDestroy()
        {
            _playerData.DataChanged -= OnPlayerSpeedBoosted;
        }

        public void Init(PlayerDataChanger playerData)
        {
            _playerData = playerData != null ? playerData : throw new ArgumentNullException(nameof(playerData));
            _playerData.DataChanged += OnPlayerSpeedBoosted;
            OnPlayerSpeedBoosted();
        }

        private void OnPlayerSpeedBoosted()
        {
            _speedTextLabel.text = _playerData.Speed.ToString("0.00");
            _pointsTextLavel.text = _playerData.Points.ToString();
        }
    }
}