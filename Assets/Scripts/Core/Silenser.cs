using Agava.WebUtility;
using System;
using UnityEngine;

public class Silenser : MonoBehaviour
{
    private GameState _gameState;
    private bool _inApp = true;

    private void OnEnable()
    {
        _gameState = new(1f, 1f, false);
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        Application.focusChanged += OnFocusChanged;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        Application.focusChanged -= OnFocusChanged;
    }

    private void OnFocusChanged(bool isFocused)
    {
        if (isFocused == false)
            PauseGame();
        else
            UnpauseGame();
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        if (inBackground)
            PauseGame();
    }

    private void UnpauseGame()
    {
        if(_gameState == null)
            throw new NullReferenceException(nameof(_gameState));

        AudioListener.pause = _gameState.IsPausing;
        AudioListener.volume = _gameState.Volume;
        Time.timeScale = _gameState.TimeScale;
        _inApp = true;
    }

    private void PauseGame()
    {
        if (_inApp)
        {
            _gameState = new(Time.timeScale, AudioListener.volume, AudioListener.pause);
            _inApp = false;
        }

        AudioListener.pause = true;
        AudioListener.volume = 0;
        Time.timeScale = 0;
    }

    private class GameState
    {
        private float _timeScale;
        private float _volume;
        private bool _isPausing;

        public GameState(float timeScale, float volume, bool isPausing)
        {
            _timeScale = timeScale >= 0f && timeScale <= 1f ? timeScale : throw new ArgumentOutOfRangeException(nameof(timeScale));
            _volume = volume >= 0f && volume <= 1f ? volume : throw new ArgumentOutOfRangeException(nameof(volume));
            _isPausing = isPausing;
        }

        public float TimeScale => _timeScale;
        public float Volume => _volume;
        public bool IsPausing => _isPausing;
    }
}
