using Agava.YandexGames;
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
        while( YandexGamesSdk.IsInitialized == false ) 
            yield return YandexGamesSdk.Initialize();
        
        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.StartAuthorizationPolling(1500);
        
        //yield return new WaitForSeconds(5);
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }
}
