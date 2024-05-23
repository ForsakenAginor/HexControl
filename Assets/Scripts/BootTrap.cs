using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootTrap : MonoBehaviour
{
    private Dictionary<string, string> _languages = new Dictionary<string, string>() { { "ru", "Russian" }, {"en", "English" }, {"tr", "Turkish" } };

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        // Always wait for it if invoking something immediately in the first scene.
        while( YandexGamesSdk.IsInitialized == false ) 
            yield return YandexGamesSdk.Initialize();

        string language = YandexGamesSdk.Environment.i18n.lang;
        LeanLocalization.SetCurrentLanguageAll(_languages[language]);
        LeanLocalization.UpdateTranslations();
        /*
        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.StartAuthorizationPolling(1500);
        */
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }
}
