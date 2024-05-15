using UnityEngine;
using UnityEngine.UI;

public class SoundInitializer : MonoBehaviour
{
    [Header("AudioSources")]
    [SerializeField] private AudioSource[] _allSources;
    [SerializeField] private AudioSource[] _effectsSources;
    [SerializeField] private AudioSource[] _musicSources;

    [Header("Sliders")]
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _effectsVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;

    private void Start()
    {
        _masterVolumeSlider.value = AudioData.Instance.MasterVolumeValue;
        _effectsVolumeSlider.value = AudioData.Instance.EffectVolumeValue;
        _musicVolumeSlider.value = AudioData.Instance.MusicVolumeValue;

        VolumeChanger masterChanger = new(_allSources, AudioData.Instance.MasterVolumeValue);
        VolumeChanger effectsChanger = new(_effectsSources, AudioData.Instance.EffectVolumeValue);
        VolumeChanger musicChanger = new(_musicSources, AudioData.Instance.MusicVolumeValue);
        VolumeChangeView masterChangerView = new(masterChanger, _masterVolumeSlider);
        VolumeChangeView effectsChangerView = new(effectsChanger, _effectsVolumeSlider);
        VolumeChangeView musicChangerView = new(musicChanger, _musicVolumeSlider);
    }

    public void SaveSettings()
    {
        AudioData.Instance.SaveChanges(_masterVolumeSlider.value, _effectsVolumeSlider.value, _musicVolumeSlider.value);
    }
}
