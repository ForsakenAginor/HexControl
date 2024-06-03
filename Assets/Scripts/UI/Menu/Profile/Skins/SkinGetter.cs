using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkinGetter
{
    private Button _button;
    private Hatter _hatter;
    private EventSystem _eventSystem;

    public SkinGetter(Hatter hatter, Button button)
    {
        _eventSystem = EventSystem.current != null ? EventSystem.current : throw new NullReferenceException(nameof(_eventSystem));
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
       // _eventSystem.gameObject.SetActive(false);
        Time.timeScale = 0;
        AudioListener.pause = true;
        AudioListener.volume = 0f;
    }

    private void OnRewardCallback()
    {
        _eventSystem.gameObject.SetActive(true);
        _hatter.TryEarnRandomHat(out _);
        OnCloseCallback();
    }

    private void OnCloseCallback()
    {
        _eventSystem.gameObject.SetActive(true);
        Time.timeScale = 1;
        AudioListener.pause = false;
        AudioListener.volume = 1f;
    }
}
