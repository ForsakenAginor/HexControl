using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _language;

    private void OnEnable()
    {
        _button.onClick.AddListener(SwitchLanguage);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void SwitchLanguage()
    {
        LeanLocalization.SetCurrentLanguageAll(_language);
        LeanLocalization.UpdateTranslations();
    }
}
