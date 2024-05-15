using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinGetter : MonoBehaviour
{
    [SerializeField] private Button _button;

    private Hatter _hatter;

    private void OnEnable()
    {
        _button.onClick.AddListener(GetSkin);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(GetSkin);
    }

    public void Init(Hatter hatter)
    {
        _hatter = hatter != null ? hatter : throw new ArgumentNullException(nameof(hatter));
    }

    private void GetSkin()
    {
        _hatter.TryEarnRandomHat(out _);
    }
}
