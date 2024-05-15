using System;
using UnityEngine;

public class AudioData : MonoBehaviour
{
    private const string MasterVolumeVariableName = nameof(MasterVolumeValue);
    private const string EffectsVolumeVariableName = nameof(EffectVolumeValue);
    private const string MusicVolumeVariableName = nameof(MusicVolumeValue);

    private float _masterVolumeValue = 1f;
    private float _effectsVolumeValue = 1f;
    private float _musicVolumeValue = 1f;

    public static AudioData Instance { get; private set; }
    public float MasterVolumeValue => _masterVolumeValue;
    public float EffectVolumeValue => _effectsVolumeValue;
    public float MusicVolumeValue => _musicVolumeValue;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
        Init();
    }

    public void SaveChanges(float masterVolume, float effectVolume, float musicVolume)
    {
        if(masterVolume < 0f || masterVolume > 1f)
            throw new ArgumentOutOfRangeException(nameof(masterVolume));

        if(effectVolume < 0f || effectVolume > 1f)
            throw new ArgumentOutOfRangeException(nameof(effectVolume));

        if(musicVolume < 0f || musicVolume > 1f)
            throw new ArgumentOutOfRangeException(nameof(musicVolume));

        _masterVolumeValue = masterVolume;
        _effectsVolumeValue = effectVolume;
        _musicVolumeValue = musicVolume;

        Save();
    }

    private void Init()
    {
        if (PlayerPrefs.HasKey(MasterVolumeVariableName))
            _masterVolumeValue = PlayerPrefs.GetFloat(MasterVolumeVariableName);

        if (PlayerPrefs.HasKey(EffectsVolumeVariableName))
            _effectsVolumeValue = PlayerPrefs.GetFloat(EffectsVolumeVariableName);

        if (PlayerPrefs.HasKey(MusicVolumeVariableName))
            _musicVolumeValue = PlayerPrefs.GetFloat(MusicVolumeVariableName);
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(MasterVolumeVariableName, _masterVolumeValue);
        PlayerPrefs.SetFloat(EffectsVolumeVariableName, _effectsVolumeValue);
        PlayerPrefs.SetFloat(MusicVolumeVariableName, _musicVolumeValue);
        PlayerPrefs.Save();
    }
}
