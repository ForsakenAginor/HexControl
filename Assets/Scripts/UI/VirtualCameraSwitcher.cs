using System;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class VirtualCameraSwitcher
    {
        private readonly GameObject _followCamera;
        private readonly GameObject _celebratingCamera;
        private readonly ConquestMonitor _monitor;

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

        private void OnPlayerWon(int nonmatterValue)
        {
            _followCamera.SetActive(false);
            _celebratingCamera.SetActive(true);
        }
    }
}