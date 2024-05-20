using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class LevelUnlocker
{
    private Scenes _currentScene;
    private Scenes _nextScene;

    public Scenes NextScene => _nextScene;
    public Scenes CurrentScene => _currentScene;

    public LevelUnlocker()
    {
        if (Enum.TryParse(SceneManager.GetActiveScene().name, out _currentScene) == false)
            throw new Exception("Wrong scene name");

        int levelsQuantity = Enum.GetNames(typeof(Scenes)).Length - 1;

        if ((int)_currentScene == levelsQuantity)
            _nextScene = _currentScene;
        else
            _nextScene = (Scenes)((int)_currentScene + 1);
    }

    public void UnlockNextLevel()
    {
        if (_currentScene == _nextScene)
            return;

        var levels = LevelData.Instance.Levels.ToList();
        levels.Add(_nextScene);
        LevelData.Instance.Save(levels);
    }
}
