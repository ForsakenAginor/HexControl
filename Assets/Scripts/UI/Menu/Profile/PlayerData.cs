using System;
using UnityEngine;

namespace Assets.Scripts.UI.Menu.Profile
{
    public class PlayerData
    {
        private const string PointsVariableName = "Points";
        private const string SpeedVariableName = "Speed";

        public int GetPoints()
        {
            if (PlayerPrefs.HasKey(PointsVariableName))
                return PlayerPrefs.GetInt(PointsVariableName);
            else
                return 0;
        }

        public void SavePoints(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            PlayerPrefs.SetInt(PointsVariableName, value);
            PlayerPrefs.Save();
        }

        public float GetSpeed()
        {
            if (PlayerPrefs.HasKey(SpeedVariableName))
                return PlayerPrefs.GetFloat(SpeedVariableName);
            else
                return 0;
        }

        public void SaveSpeed(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            PlayerPrefs.SetFloat(SpeedVariableName, value);
            PlayerPrefs.Save();
        }
    }
}