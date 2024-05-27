using System;
using UnityEngine;
using UnityEngine.UI;

public class UISlideSoundEffect : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceOpener;
    [SerializeField] private Button _closeButton;
    [SerializeField] private AudioSource _audioSourceCloser;

    private void OnEnable()
    {
        _audioSourceOpener.Play();
        _closeButton.onClick.AddListener(PlayCloseEffect);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(PlayCloseEffect);
    }

    private void PlayCloseEffect()
    {
        _audioSourceCloser.Play();
    }
}
