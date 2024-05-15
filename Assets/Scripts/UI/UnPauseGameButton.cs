using UnityEngine;
using UnityEngine.UI;

public class UnPauseGameButton : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private GameObject _buttonHolder;
    [SerializeField] private Canvas[] _canvasesThatWillBeEnabled;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(Unpause);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(Unpause);
    }

    private void Unpause()
    {
        _buttonHolder.SetActive(false);

        foreach(Canvas canvas in _canvasesThatWillBeEnabled)
            canvas.gameObject.SetActive(true);

        Time.timeScale = 1f;
    }
}
