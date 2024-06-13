using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneChangeButtonCreator : MonoBehaviour
{
    [SerializeField] private SceneChanger _prefab;
    [SerializeField] private Transform _holder;

    private readonly LevelData _levelData = new();

    private void Start()
    {
        List<Scenes> unlockedLevels;
        var levels = _levelData.Levels;

        if (levels != null)
        {
            unlockedLevels = levels.OrderByDescending(o => (int)o).ToList();
        }
        else
        {
            unlockedLevels = new List<Scenes>() { Scenes.FirstLevel };
            _levelData.Levels = unlockedLevels;
        }

        foreach(var level in unlockedLevels)        
            Instantiate(_prefab, _holder).Init(level);        
    }
}
