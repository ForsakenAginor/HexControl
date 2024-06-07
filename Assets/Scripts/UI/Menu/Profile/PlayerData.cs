using System;
using UnityEngine;

public class PlayerData
{
    private const string PointsVariableName = nameof(Points);
    private const string SpeedVariableName = nameof(Speed);

    public int Points
    {
        get
        {
            if (PlayerPrefs.HasKey(PointsVariableName))
                return PlayerPrefs.GetInt(PointsVariableName);
            else
                return 0;
        }

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            PlayerPrefs.SetInt(PointsVariableName, value);
            PlayerPrefs.Save();
        }
    }

    public float Speed
    {
        get
        {
            if (PlayerPrefs.HasKey(SpeedVariableName))
                return PlayerPrefs.GetFloat(SpeedVariableName);
            else
                return 0;
        }

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            PlayerPrefs.SetFloat(SpeedVariableName, value);
            PlayerPrefs.Save();
        }
    }
}
