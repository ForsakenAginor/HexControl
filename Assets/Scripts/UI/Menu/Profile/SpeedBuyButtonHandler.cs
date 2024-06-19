using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu.Profile
{
    public class SpeedBuyButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _costTextLabel;

        private PlayerDataChanger _playerData;

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(BuySpeed);
        }

        public void Init(PlayerDataChanger playerData)
        {
            _playerData = playerData != null ? playerData : throw new ArgumentNullException(nameof(playerData));
            _button.onClick.AddListener(BuySpeed);
            _costTextLabel.text = _playerData.SpeedBoostCost.ToString();

            if (_playerData.Speed >= _playerData.MaxSpeed)
            {
                _costTextLabel.text = "-";
                _button.interactable = false;
                return;
            }
        }

        private void BuySpeed()
        {
            if (_playerData.Speed >= _playerData.MaxSpeed)
            {
                _costTextLabel.text = "-";
                _button.interactable = false;
                return;
            }

            _playerData.TryBoostSpeed();
            _costTextLabel.text = _playerData.SpeedBoostCost.ToString();
        }
    }
}