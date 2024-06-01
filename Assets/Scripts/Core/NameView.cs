using Lean.Localization;
using TMPro;
using UnityEngine;

public class NameView : MonoBehaviour
{
    [SerializeField] private Conquestor _conquestor;
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private LeanLocalizedTextMeshProUGUI _localizedText;

    private void Start()
    {
        _localizedText.TranslationName = _conquestor.Name;
        _localizedText.UpdateLocalization();
        //_textField.text = _conquestor.Name;
    }
}
