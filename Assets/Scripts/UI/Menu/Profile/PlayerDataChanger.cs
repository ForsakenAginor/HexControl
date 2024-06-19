using System;
using UnityEngine;

namespace Assets.Scripts.UI.Menu.Profile
{
    public class PlayerDataChanger
    {
        private readonly float _speedBoostValue;
        private readonly float _baseSpeed;
        private readonly int _baseSpeedBoostCost;
        private readonly float _multiplier;
        private readonly float _maxSpeed;
        private readonly PlayerData _playerData = new ();
        private int _speedBoostCost;
        private int _points;
        private float _speed;

        public PlayerDataChanger(float speedBoostValue, int baseSpeedBoostCost, float baseSpeed, float multiplier, float maxSpeed)
        {
            _speedBoostValue = speedBoostValue > 0 ? speedBoostValue : throw new ArgumentOutOfRangeException(nameof(speedBoostValue));
            _baseSpeedBoostCost = baseSpeedBoostCost > 0 ? baseSpeedBoostCost : throw new ArgumentOutOfRangeException(nameof(baseSpeedBoostCost));
            _baseSpeed = baseSpeed > 0 ? baseSpeed : throw new ArgumentOutOfRangeException(nameof(baseSpeed));
            _multiplier = multiplier >= 1 ? multiplier : throw new ArgumentOutOfRangeException(nameof(multiplier));
            _maxSpeed = maxSpeed > 0 ? maxSpeed : throw new ArgumentOutOfRangeException(nameof(maxSpeed));
            _points = _playerData.GetPoints();
            float savedSpeed = _playerData.GetSpeed();
            _speed = savedSpeed > 0 && savedSpeed <= _maxSpeed ? savedSpeed : _baseSpeed;

            CalculateBoostCost();
            SaveData();
        }

        public event Action SpeedBoosted;

        public int Points => _points;

        public int SpeedBoostCost => _speedBoostCost;

        public float Speed => _speed;

        public float MaxSpeed => _maxSpeed;

        public void Add(int points)
        {
            if (points <= 0)
                throw new ArgumentOutOfRangeException(nameof(points));

            _points += points;
            SaveData();
        }

        public bool TryBoostSpeed()
        {
            if (_points < _speedBoostCost || _speed >= _maxSpeed)
                return false;

            _points -= _speedBoostCost;
            _speed += _speedBoostValue;
            SpeedBoosted?.Invoke();
            CalculateBoostCost();
            SaveData();

            return true;
        }

        private void CalculateBoostCost()
        {
            int upgrades = Mathf.RoundToInt((_speed - _baseSpeed) / _speedBoostValue);
            _speedBoostCost = (int)(_baseSpeedBoostCost * Mathf.Pow(_multiplier, upgrades));
        }

        private void SaveData()
        {
            _playerData.SavePoints(_points);
            _playerData.SaveSpeed(_speed);
        }
    }
}