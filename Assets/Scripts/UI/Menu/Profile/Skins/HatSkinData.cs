using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HatSkinData
{
    private const string ActiveHatVariableName = nameof(ActiveHatVariableName);
    private const string OwnedHatsVariableName = nameof(OwnedHatsVariableName);

    public IEnumerable<Hats> OwnedHats
    {
        get
        {
            if (PlayerPrefs.HasKey(OwnedHatsVariableName))
                return PlayerPrefs.GetString(OwnedHatsVariableName).Trim().Split(' ').Select(o => (Hats)Convert.ToInt32(o));
            else
                return new List<Hats>() { Hats.None };
        }

        set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            StringBuilder builder = new();
            string divider = " ";

            foreach (var hat in value)
                builder.Append($"{(int)hat}{divider}");

            PlayerPrefs.SetString(OwnedHatsVariableName, builder.ToString());
            PlayerPrefs.Save();
        }
    }

    public Hats ActiveHat
    {
        get
        {
            if (PlayerPrefs.HasKey(ActiveHatVariableName))
                return (Hats)PlayerPrefs.GetInt(ActiveHatVariableName);
            else
                return Hats.None;
        }

        set
        {
            PlayerPrefs.SetInt(ActiveHatVariableName, (int)value);
            PlayerPrefs.Save();
        }
    }
}