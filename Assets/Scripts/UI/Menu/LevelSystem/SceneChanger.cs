using Agava.YandexGames;
using System;
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
    private Silencer _silencer;

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

        if (_silencer == null)
            throw new Exception(nameof(_silencer));
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
        _button.interactable = false;

        if (_nextScene != Scenes.MainMenu && _nextScene != Scenes.FirstLevel)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            AudioListener.volume = 0f;
            InterstitialAd.Show(null, OnCloseAdvertise);
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(_nextScene.ToString());
        }
    }

    private void OnCloseAdvertise(bool _)
    {
        AudioListener.pause = false;
        AudioListener.volume = 1f;
        Time.timeScale = 0f;
        _silencer.SetGameState(Time.timeScale, AudioListener.volume, AudioListener.pause);
        SceneManager.LoadScene(_nextScene.ToString());
    }
}