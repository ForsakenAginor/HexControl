using System;
using UnityEngine;

public class TutorialData : MonoBehaviour
{
    private const string TutorialVariableName = nameof(IsTutorialCompleted);

    private bool _isTutorialCompleted;

    public static TutorialData Instance { get; private set; }
    public bool IsTutorialCompleted => _isTutorialCompleted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
        Init();
    }

    public void Save(bool isCompleted)
    {
        _isTutorialCompleted = isCompleted;
        Save();
    }

    private void Init()
    {
        if (PlayerPrefs.HasKey(TutorialVariableName))
            _isTutorialCompleted = Convert.ToBoolean(PlayerPrefs.GetInt(TutorialVariableName));
        else
            _isTutorialCompleted = false;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(TutorialVariableName, Convert.ToInt32(_isTutorialCompleted));
        PlayerPrefs.Save();
    }
}
