using System;
using UnityEngine.UI;

public class SkinGetter
{
    private Button _button;
    private Hatter _hatter;

    public SkinGetter(Hatter hatter, Button button)
    {
        _hatter = hatter != null ? hatter : throw new ArgumentNullException(nameof(hatter));
        _button = button != null ? button : throw new ArgumentNullException( nameof(button));
        _button.onClick.AddListener(GetSkin);
    }

    ~SkinGetter()
    {
        _button.onClick.RemoveListener(GetSkin);
    }

    private void GetSkin()
    {
        _hatter.TryEarnRandomHat(out _);
    }
}
