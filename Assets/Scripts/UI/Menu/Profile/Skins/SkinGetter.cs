using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinGetter
{
    private Button _button;
    private Hatter _hatter;

    public SkinGetter(Hatter hatter, Button button)
    {
        _hatter = hatter != null ? hatter : throw new ArgumentNullException(nameof(hatter));
        _button = button != null ? button : throw new ArgumentNullException( nameof(button));
        _button.onClick.AddListener(ShowVideoAd);
    }

    ~SkinGetter()
    {
        _button.onClick.RemoveListener(ShowVideoAd);
    }

    private void ShowVideoAd()
    {
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);
    }

    private void OnOpenCallback()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        AudioListener.volume = 0f;
    }

    private void OnRewardCallback() => _hatter.TryEarnRandomHat(out _);

    private void OnCloseCallback()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        AudioListener.volume = 1f;
    }
}
