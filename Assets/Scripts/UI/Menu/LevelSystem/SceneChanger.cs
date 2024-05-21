using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;

    private Scenes _nextScene;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeScene);        
    }

    public void Init(Scenes scene)
    {
        _nextScene = scene;

        if(_buttonText != null)
            _buttonText.text = $"Level {(int)scene}";
    }

    public void ChangeScene()
    {
        InterstitialAd.Show();
        SceneManager.LoadScene(_nextScene.ToString());
    }
}
