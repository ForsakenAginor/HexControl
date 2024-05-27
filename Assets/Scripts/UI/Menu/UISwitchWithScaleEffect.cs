using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;

public class UISwitchWithScaleEffect : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _holder;
    [SerializeField] private GameObject _target;

    private float _scaleX = 1f;
    private Vector3 _startingScale = new(0, 1, 1);
    private Transform _transform;

    private void OnValidate()
    {
        _duration = Mathf.Clamp01(_duration);
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _transform.localScale = _startingScale;
    }

    private void OnEnable()
    {
        _transform.DOScaleX(_scaleX, _duration);
        _closeButton.onClick.AddListener(CloseMenu);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(CloseMenu);
    }

    private void CloseMenu()
    {
        _transform.DOScaleX(_startingScale.x, _duration).SetEase(Ease.InQuad);
        StartCoroutine(WaitTween());
    }

    private IEnumerator WaitTween()
    {
        WaitForSeconds delay = new(_duration);
        yield return delay;
        _target.SetActive(true);
        _holder.SetActive(false);
    }
}
