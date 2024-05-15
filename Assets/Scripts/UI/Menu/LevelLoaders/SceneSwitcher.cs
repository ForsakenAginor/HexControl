using UnityEngine;
using UnityEngine.UI;

public abstract class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private Button _toggleButton;

    private void OnEnable()
    {
        _toggleButton.onClick.AddListener(LoadScene);
    }

    private void OnDisable()
    {
        _toggleButton.onClick.RemoveListener(LoadScene);
    }

    protected abstract void LoadScene();
}
