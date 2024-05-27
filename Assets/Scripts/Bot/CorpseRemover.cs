using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CorpseRemover : MonoBehaviour
{
    [SerializeField] private Canvas _personalCanvas;
    [SerializeField] private BotAnimationHandler _animationHandler;
    [SerializeField] private GameObject _botModel;

    private float _duration = 1f;

    public void RemoveCorpse()
    {
        _personalCanvas.gameObject.SetActive(false);
        _animationHandler.enabled = false;
        transform.DOMove(transform.position + Vector3.down, _duration).SetEase(Ease.Linear);
        StartCoroutine(DisableModelWithDelay());
    }

    private IEnumerator DisableModelWithDelay()
    {
        WaitForSeconds delay = new(_duration);
        yield return delay;
        _botModel.SetActive(false);
    }
}
