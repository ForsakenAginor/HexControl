using System.Collections;
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
            //YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            string language = "en";
            yield return null;

            LocalizationInitializer localizationInitializer = new ();
            localizationInitializer.ApplyLocalization(language);
            SceneManager.LoadScene(Scenes.MainMenu.ToString());
        }
    }
}