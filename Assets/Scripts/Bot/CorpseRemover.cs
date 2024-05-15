using DG.Tweening;
using UnityEngine;

public class CorpseRemover : MonoBehaviour
{
    [SerializeField] private Canvas _personalCanvas;
    [SerializeField] private BotAnimationHandler _animationHandler;

    public void RemoveCorpse()
    {
        _personalCanvas.gameObject.SetActive(false);
        _animationHandler.enabled = false;
        float duration = 1f;
        transform.DOMove(transform.position + Vector3.down, duration).SetEase(Ease.Linear);
    }
}
