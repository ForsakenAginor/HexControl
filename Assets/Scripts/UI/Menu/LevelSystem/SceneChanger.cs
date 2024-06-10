using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;

    private Silencer _silencer;
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

    private void Start()
    {
        _silencer = FindAnyObjectByType<Silencer>();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeScene);
    }

    public void Init(Scenes scene)
    {
        _nextScene = scene;

        if (_buttonText != null)
            _buttonText.text = $"{(int)scene}";
    }

    public void ChangeScene()
    {
        if (_nextScene != Scenes.MainMenu && _nextScene != Scenes.FirstLevel)
        {
            _silencer.gameObject.SetActive(false);
            InterstitialAd.Show(null, OnCloseAdvertise);
        }

        SceneManager.LoadScene(_nextScene.ToString());
    }

    private void OnCloseAdvertise(bool _)
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        AudioListener.volume = 1f;
        _silencer.gameObject.SetActive(true);
    }
}