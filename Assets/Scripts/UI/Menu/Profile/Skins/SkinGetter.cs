using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu.Profile.Skins
{
    public class SkinGetter
    {
        private readonly Button _button;
        private readonly Hatter _hatter;
        private readonly PlayerDataChanger _playerDataChanger;

        public SkinGetter(Hatter hatter, Button button, PlayerDataChanger playerDataChanger)
        {
            _hatter = hatter != null ? hatter : throw new ArgumentNullException(nameof(hatter));
            _button = button != null ? button : throw new ArgumentNullException(nameof(button));
            _playerDataChanger = playerDataChanger != null ?
                playerDataChanger : throw new ArgumentNullException(nameof(playerDataChanger));

            _button.onClick.AddListener(BuyHat);
        }

        ~SkinGetter()
        {
            _button.onClick.RemoveListener(BuyHat);
        }

        public event Action AllSkinsObtained;

        private void BuyHat()
        {
            _playerDataChanger.TryBuyHat();

            if (_hatter.IsAllHatsObtained)
            {
                _button.onClick.RemoveListener(BuyHat);
                _button.gameObject.SetActive(false);
                AllSkinsObtained?.Invoke();
            }
        }
    }
}