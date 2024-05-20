using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private const string PointsVariableName = nameof(Points);
    private const string SpeedVariableName = nameof(Speed);

    private int _points;
    private float _speed;

    public static PlayerData Instance { get; private set; }
    public int Points => _points;
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

    public void Save(int points, float speed)
    {
        _points = points >= 0 ? points : throw new ArgumentOutOfRangeException(nameof(points));
        _speed = speed > 0 ? speed : throw new ArgumentOutOfRangeException(nameof(speed));
        Save();
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
