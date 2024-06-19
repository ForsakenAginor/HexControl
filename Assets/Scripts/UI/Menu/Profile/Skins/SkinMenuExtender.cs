using System;
using UnityEngine;

namespace Assets.Scripts.UI.Menu.Profile.Skins
{
    public class SkinMenuExtender
    {
        private readonly SkinGetter _skinGetter;
        private readonly RectTransform _extendedPanel;
        private readonly float _targetYPosition;

        public SkinMenuExtender(SkinGetter skinGetter, RectTransform extendedPanel, float targetYPosition)
        {
            _skinGetter = skinGetter != null ? skinGetter : throw new ArgumentNullException(nameof(skinGetter));
            _extendedPanel = extendedPanel != null ? extendedPanel : throw new ArgumentNullException(nameof(extendedPanel));
            _targetYPosition = targetYPosition;

            _skinGetter.AllSkinsObtained += OnAllSkinObtained;
        }

        ~SkinMenuExtender() => _skinGetter.AllSkinsObtained -= OnAllSkinObtained;

        private void OnAllSkinObtained()
        {
            Vector2 anchor = _extendedPanel.anchorMin;
            anchor.y = _targetYPosition;
            _extendedPanel.anchorMin = anchor;
        }
    }
}