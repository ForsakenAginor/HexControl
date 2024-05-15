using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HatSkinData : MonoBehaviour
{
    private const string ActiveHatVariableName = nameof(ActiveHatVariableName);
    private const string OwnedHatsVariableName = nameof(OwnedHatsVariableName);

    private Hats _activeHat;
    private IEnumerable<Hats> _ownedHats;

    public static HatSkinData Instance { get; private set; }
    public IEnumerable<Hats> OwnedHats => _ownedHats;
    public Hats ActiveHat => _activeHat;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
        Init();
    }

    public void SaveChanges(IEnumerable<Hats> ownedHats, Hats activeHat)
    {
        _ownedHats = ownedHats != null ? ownedHats.ToArray() : throw new ArgumentNullException(nameof(ownedHats));
        _activeHat = activeHat;

        Save();
    }

    private void Init()
    {
        if (PlayerPrefs.HasKey(ActiveHatVariableName))
            _activeHat = (Hats)PlayerPrefs.GetInt(ActiveHatVariableName);
        else
            _activeHat = Hats.None;

        if (PlayerPrefs.HasKey(OwnedHatsVariableName))
            _ownedHats = PlayerPrefs.GetString(OwnedHatsVariableName).Trim().Split(' ').Select(o => (Hats)Convert.ToInt32(o));
        else
            _ownedHats = new List<Hats>() { Hats.None };
    }

    private void Save()
    {
        PlayerPrefs.SetInt(ActiveHatVariableName, (int)_activeHat);

        StringBuilder builder = new();
        string divider = " ";

        foreach(var hat in _ownedHats)
            builder.Append($"{(int)hat}{divider}");        
        
        PlayerPrefs.SetString(OwnedHatsVariableName, builder.ToString() );

        PlayerPrefs.Save();
    }
}