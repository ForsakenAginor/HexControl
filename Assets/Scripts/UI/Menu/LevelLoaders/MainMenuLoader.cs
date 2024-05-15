using IJunior.TypedScenes;
using UnityEngine;

public class MainMenuLoader : SceneSwitcher
{
    protected override void LoadScene()
    {
        Time.timeScale = 1.0f;
        MainMenu.Load();
    }
}
