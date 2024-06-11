using Lean.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ConquestBar : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedText;
    [SerializeField] private TextMeshProUGUI _percentText;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void ChangeSliderValue(float value)
    {
        value = Mathf.Clamp01(value);
        _slider.value = value;
    }


    public void ChangeColor(Color color)
    {
        _barImage.color = color;
    }

    public void ChangeText(string text, float part)
    {
        //_localizedText.TranslationName = text;
        //_localizedText.UpdateLocalization();
        _percentText.text = $"{part * 100: 0.00}%";
    }
}
