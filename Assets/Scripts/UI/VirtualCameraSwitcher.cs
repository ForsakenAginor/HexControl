using System;
using UnityEngine;

public class VirtualCameraSwitcher
{
    private GameObject _followCamera;
    private GameObject _celebratingCamera;
    private ConquestMonitor _monitor;

    public VirtualCameraSwitcher(GameObject followCamera, GameObject celebratingCamera, ConquestMonitor monitor)
    {
        _followCamera = followCamera != null ? followCamera : throw new ArgumentNullException(nameof(followCamera));
        _celebratingCamera = celebratingCamera != null ? celebratingCamera : throw new ArgumentNullException(nameof(celebratingCamera));
        _monitor = monitor != null ? monitor : throw new ArgumentNullException(nameof(monitor));
        _monitor.PlayerWon += OnPlayerWon;
    }

    ~VirtualCameraSwitcher()
    {
        _monitor.PlayerWon -= OnPlayerWon;
    }

    private void OnPlayerWon(int _)
    {
        _followCamera.SetActive(false);
        _celebratingCamera.SetActive(true);
    }
}
