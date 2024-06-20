using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.BotLogic
{
    public class CorpseRemover : MonoBehaviour
    {
        private readonly float _duration = 1f;

        [SerializeField] private Canvas _personalCanvas;
        [SerializeField] private BotAnimationHandler _animationHandler;
        [SerializeField] private GameObject _botModel;

        public void RemoveCorpse()
        {
            _personalCanvas.gameObject.SetActive(false);
            _animationHandler.enabled = false;
            transform.DOMove(transform.position + Vector3.down, _duration).SetEase(Ease.Linear);
            StartCoroutine(DisableModelWithDelay());
        }

        private IEnumerator DisableModelWithDelay()
        {
            WaitForSeconds delay = new (_duration);
            yield return delay;
            _botModel.SetActive(false);
        }
    }
}