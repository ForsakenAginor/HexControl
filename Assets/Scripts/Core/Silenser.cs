using Agava.WebUtility;
using System;
using UnityEngine;

public class Silenser : MonoBehaviour
{
    private GameState _gameState;

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;        
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        if (inBackground)
        {
            _gameState = new(Time.timeScale, AudioListener.volume, AudioListener.pause);
            AudioListener.pause = true;
            AudioListener.volume = 0;
            Time.timeScale = 0;
        }
        else
        {
            AudioListener.pause = _gameState.IsPausing;
            AudioListener.volume = _gameState.Volume;
            Time.timeScale = _gameState.TimeScale;
        }
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
