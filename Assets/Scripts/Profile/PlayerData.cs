using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private const string PointsVariableName = nameof(Points);
    private const string SpeedVariableName = nameof(Speed);

    private readonly float _speedBoostValue = 0.1f;
    private readonly int _speedBoostCost = 10;
    private int _points = 0;
    private float _speed = 4f;

    public event Action SpeedBoosted;

    public static PlayerData Instance { get; private set; }
    public int Points => _points;
    public int SpeedBoostCost => _speedBoostCost;
    public float Speed => _speed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
        Init();
    }

    public void Add(int points)
    {
        if(points <= 0)
            throw new ArgumentOutOfRangeException(nameof(points));

        _points += points;
        Save();
    }

    public bool TryBoostSpeed()
    {
        if(_points < _speedBoostCost)
            return false;

        _points -= _speedBoostCost;
        _speed += _speedBoostValue;
        SpeedBoosted?.Invoke();
        Save();

        return true;
    }

    private void Init()
    {
        if(PlayerPrefs.HasKey(PointsVariableName))
            _points = PlayerPrefs.GetInt(PointsVariableName);

        if(PlayerPrefs.HasKey(SpeedVariableName))
            _speed = PlayerPrefs.GetFloat(SpeedVariableName);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(PointsVariableName, _points);
        PlayerPrefs.SetFloat(SpeedVariableName, _speed);
        PlayerPrefs.Save();
    }
}
