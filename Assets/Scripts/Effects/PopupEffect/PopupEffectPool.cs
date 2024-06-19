using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Effects.PopupEffect
{
    public class PopupEffectPool : MonoBehaviour
    {
        private readonly Stack<PopupEffectView> _pool = new ();
        private PopupEffectView _prefab;
        private Transform _parent;
        private PartCalculator _partCalculator;

        private void OnDestroy()
        {
            _partCalculator.ScoreAdded -= OnScoreAdded;
        }

        public void Init(PartCalculator partCalculator, PopupEffectView prefab, Transform parentCanvas)
        {
            _partCalculator = partCalculator != null ? partCalculator : throw new ArgumentNullException(nameof(partCalculator));
            _prefab = prefab != null ? prefab : throw new ArgumentNullException(nameof(prefab));
            _parent = parentCanvas != null ? parentCanvas : throw new ArgumentNullException(nameof(parentCanvas));
            _partCalculator.ScoreAdded += OnScoreAdded;
        }

        public void ReturnToPool(PopupEffectView popupEffectView)
        {
            if (popupEffectView == null)
                throw new ArgumentNullException(nameof(popupEffectView));

            if (_pool.Contains(popupEffectView) == false)
                _pool.Push(popupEffectView);
        }

        private void OnScoreAdded(float value)
        {
            if (_pool.Count == 0)
            {
                PopupEffectView popupEffectView = Instantiate(_prefab, _parent);
                popupEffectView.Init(value, this);
            }
            else
            {
                _pool.Pop().Init(value, this);
            }
        }
    }
}