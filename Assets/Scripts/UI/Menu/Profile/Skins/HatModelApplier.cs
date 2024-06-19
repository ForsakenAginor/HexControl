using System;
using UnityEngine;

namespace Assets.Scripts.UI.Menu.Profile.Skins
{
    public class HatModelApplier : MonoBehaviour
    {
        [SerializeField] private Transform _hatSocket;

        private Hatter _hatter;
        private GameObject _currentHat;

        private void OnDestroy()
        {
            _hatter.ActiveHatChanged -= OnActiveHatChanged;
        }

        public void Init(Hatter hatter)
        {
            _hatter = hatter != null ? hatter : throw new ArgumentNullException(nameof(hatter));
            _currentHat = Instantiate(hatter.ActiveHat.Model, _hatSocket);
            _hatter.ActiveHatChanged += OnActiveHatChanged;
        }

        private void OnActiveHatChanged()
        {
            _currentHat.SetActive(false);
            _currentHat = Instantiate(_hatter.ActiveHat.Model, _hatSocket);
        }
    }
}