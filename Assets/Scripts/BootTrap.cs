using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootTrap : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }
    
    private IEnumerator Start()
    {    
        // Always wait for it if invoking something immediately in the first scene.
        while (YandexGamesSdk.IsInitialized == false)
            yield return YandexGamesSdk.Initialize();

        ApplyLocalization();
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }

    private static void ApplyLocalization()
    {
        const string Russian = nameof(Russian);
        const string Turkish = nameof(Turkish);
        const string English = nameof(English);
        const string CommandTurkishLanguage = "tr";
        const string CommandRussianLanguage = "ru";
        const string CommandBelorusLanguage = "be";
        const string CommandKazakhstanLanguage = "kk";
        const string CommandUzbekistanLanguage = "uz";
        const string CommandYaNeZnayChtoEtoLanguage = "uk";

        switch (YandexGamesSdk.Environment.i18n.lang)
        {
            case CommandTurkishLanguage:
                LeanLocalization.SetCurrentLanguageAll(Turkish);
                break;

            case CommandRussianLanguage:
            case CommandBelorusLanguage:
            case CommandKazakhstanLanguage:
            case CommandUzbekistanLanguage:
            case CommandYaNeZnayChtoEtoLanguage:
                LeanLocalization.SetCurrentLanguageAll(Russian);
                break;

            default:
                LeanLocalization.SetCurrentLanguageAll(English);
                break;
        }

        LeanLocalization.UpdateTranslations();
    }
}
