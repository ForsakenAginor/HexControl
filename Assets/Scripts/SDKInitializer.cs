using System.Collections;
using Agava.YandexGames;
using Assets.Scripts.Core;
using Assets.Scripts.UI.Menu.LevelSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class SDKInitializer : MonoBehaviour
    {
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if UNITY_WEB && !UNITY_EDITOR
            while (YandexGamesSdk.IsInitialized == false)
                yield return YandexGamesSdk.Initialize();

            string language = YandexGamesSdk.Environment.i18n.lang;
#elif UNITY_EDITOR
            string language = "en";
            yield return null;
#endif
            LocalizationInitializer localizationInitializer = new ();
            localizationInitializer.ApplyLocalization(language);
            SceneManager.LoadScene(Scenes.MainMenu.ToString());
        }
    }
}