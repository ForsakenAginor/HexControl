using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    private const string LevelsVariableName = nameof(Levels);

    private IEnumerable<Scenes> _levels;
    private bool _isTutorialCompleted;

    public static LevelData Instance { get; private set; }
    public IEnumerable<Scenes> Levels => _levels;
    public bool IsTutorialCompleted => _isTutorialCompleted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
        Init();
    }

    public void Save(List<Scenes> levels)
    {
        if(levels == null)
            throw new ArgumentNullException(nameof(levels));
        
        List<Scenes> list = new();

        foreach (var scene in levels)
            if(list.Contains(scene) == false)
                list.Add(scene);

        _levels = list;
        Save();
    }

    private void Init()
    {
        if(PlayerPrefs.HasKey(LevelsVariableName))
        {
            char divider = ' ';
            _levels = PlayerPrefs.GetString(LevelsVariableName).Trim().Split(divider).Select( o => (Scenes)Convert.ToInt32(o)).ToList();         
        }
    }

    private void Save()
    {
        StringBuilder builder = new();
        char divider = ' ';

        foreach(var level in _levels)        
            builder.Append($"{(int)level}{divider}");
        
        PlayerPrefs.SetString(LevelsVariableName, builder.ToString());

        PlayerPrefs.Save();
    }
}
