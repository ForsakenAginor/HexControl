using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneChangeButtonCreator : MonoBehaviour
{
    [SerializeField] private SceneChanger _prefab;
    [SerializeField] private Transform _holder;

    private void Start()
    {
        List<Scenes> unlockedLevels;
        var levels = LevelData.Instance.Levels;

        if (levels != null)
        {
            unlockedLevels = levels.OrderByDescending(o => (int)o).ToList();
        }
        else
        {
            unlockedLevels = new List<Scenes>() { Scenes.FirstLevel };
            LevelData.Instance.Save(unlockedLevels);
        }

        foreach(var level in unlockedLevels)        
            Instantiate(_prefab, _holder).Init(level);        
    }
}
