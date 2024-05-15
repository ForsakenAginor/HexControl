using IJunior.TypedScenes;

public class SecondLevelLoader : SceneSwitcher
{
    protected override void LoadScene()
    {
        SecondLevel.Load();        
    }
}
