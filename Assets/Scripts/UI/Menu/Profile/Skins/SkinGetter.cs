using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkinGetter
{
    private readonly int _hatsSkinTotalAmount = Enum.GetNames(typeof(Hats)).Length;

    private readonly Button _button;
    private readonly Hatter _hatter;

    public SkinGetter(Hatter hatter, Button button)
    {
        _hatter = hatter != null ? hatter : throw new ArgumentNullException(nameof(hatter));
        _button = button != null ? button : throw new ArgumentNullException(nameof(button));
        _button.onClick.AddListener(ShowVideoAd);
    }

    ~SkinGetter()
    {
        _button.onClick.RemoveListener(ShowVideoAd);
    }

    public event Action AllSkinsObtained;

    private void ShowVideoAd()
    {
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);
    }

    private void OnOpenCallback()
    {
        _button.interactable = false;
        Time.timeScale = 0;
        AudioListener.pause = true;
        AudioListener.volume = 0f;
    }

    private void OnRewardCallback()
    {
        _hatter.TryEarnRandomHat(out _);

        if (_hatter.Hats.Count() == _hatsSkinTotalAmount)
        {
            _button.onClick.RemoveListener(ShowVideoAd);
            _button.gameObject.SetActive(false);
            AllSkinsObtained?.Invoke();
        }
    }

    private void OnCloseCallback()
    {
        _button.interactable = true;
        Time.timeScale = 1;
        AudioListener.pause = false;
        AudioListener.volume = 1f;
    }
}
