using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Effects.PopupEffect
{
    public class PopupEffectView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private float _duration = 3f;
        [SerializeField] private float _popupHeight = 2f;

        private PopupEffectPool _pool;
        private Vector3 _startPosition;

        public void Init(float value, PopupEffectPool popupEffectPool)
        {
            _pool = popupEffectPool != null ? popupEffectPool : throw new ArgumentNullException(nameof(popupEffectPool));
            _textField.text = $"+{value:0.00}%";
            _textField.alpha = 1f;
            _startPosition = transform.localPosition;
            _textField.DOFade(0, _duration);
            transform.DOLocalMoveY(_popupHeight, _duration).SetEase(Ease.Linear);
            StartCoroutine(WaitAnimationEnd());
        }

        private IEnumerator WaitAnimationEnd()
        {
            WaitForSeconds delay = new (_duration);
            yield return delay;
            transform.localPosition = _startPosition;
            _pool.ReturnToPool(this);
        }
    }
}