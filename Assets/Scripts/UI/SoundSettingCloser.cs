using UnityEngine;
using UnityEngine.UI;

public class SoundSettingCloser : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _soundSettingsHolder;
    [SerializeField] private GameObject _buttonsCanvas;

    private void OnEnable()
    {
        _button.onClick.AddListener(CloseSettings);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(CloseSettings);
    }

    private void CloseSettings()
    {
        Time.timeScale = 1f;
        _soundSettingsHolder.SetActive(false);
        _buttonsCanvas.SetActive(true);
    }
}
