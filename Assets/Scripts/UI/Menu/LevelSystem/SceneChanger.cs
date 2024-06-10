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

        if(_silencer == null)
            throw new NullReferenceException(nameof(_silencer));
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
            InterstitialAd.Show(OnOpenAdvertise, OnCloseAdvertise);
        }

        SceneManager.LoadScene(_nextScene.ToString());
    }

    private void OnOpenAdvertise()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        AudioListener.volume = 0f;
    }


    private void OnCloseAdvertise(bool _)
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        AudioListener.volume = 1f;
        _silencer.gameObject.SetActive(true);
    }
}