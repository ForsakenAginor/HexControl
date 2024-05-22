using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class ExitButtonHandler : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Close);        
    }

    private void Close()
    {
        ReviewPopup.Open();
        Application.Quit();
    }
}
