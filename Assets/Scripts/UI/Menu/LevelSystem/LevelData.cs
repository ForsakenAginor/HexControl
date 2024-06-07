using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LevelData
{
    private const string LevelsVariableName = nameof(Levels);

    public IEnumerable<Scenes> Levels
    {
        get
        {
            if (PlayerPrefs.HasKey(LevelsVariableName))
            {
                char divider = ' ';
                return PlayerPrefs.GetString(LevelsVariableName).Trim().Split(divider).Select(o => (Scenes)Convert.ToInt32(o)).ToList();
            }
            else
            {
                return null;
            }
        }

        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            List<Scenes> list = new();

            foreach (var scene in value)
                if (list.Contains(scene) == false)
                    list.Add(scene);

            StringBuilder builder = new();
            char divider = ' ';

            foreach (var level in list)
                builder.Append($"{(int)level}{divider}");

            PlayerPrefs.SetString(LevelsVariableName, builder.ToString());
            PlayerPrefs.Save();
        }
    }
}
