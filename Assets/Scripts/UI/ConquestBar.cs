using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ConquestBar : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private TextMeshProUGUI _text;

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

    public void ChangeText(string text)
    {
        _text.text = text;
    }
}
