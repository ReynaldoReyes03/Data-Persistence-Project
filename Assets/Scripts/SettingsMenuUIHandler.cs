using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuUIHandler : MonoBehaviour {
    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer musicAudioMixer;
    [SerializeField] private AudioMixer fXAudioMixer;

    [Header("Dropdowns")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Sliders")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider fXSlider;

    [Header("Panel")]
    [SerializeField] private GameObject brightnessPanel;

    private SettingsData settingsData;

    private Resolution[] resolutions;

    private Image brightnessFilter;

    [HideInInspector] public int quality;
    [HideInInspector] public float brightnessValue;
    [HideInInspector] public float musicVolume;
    [HideInInspector] public float fxVolume;

    private void Start() {
        brightnessPanel = GameObject.FindGameObjectWithTag("BrightnessPanel").gameObject;
        brightnessFilter = brightnessPanel.GetComponent<Image>();

        SetDefaultSettings();
    }

    public void SetDefaultSettings() {
        LoadDefaultSettings();
        ConfigurateUIElements();
        RestoreDefaultValues();
    }

    private void LoadDefaultSettings() {
        settingsData = GameManager.Instance.GetSettigns();

        if (settingsData != null) {
            quality = settingsData.quality;
            brightnessValue = settingsData.brightnessValue;
            musicVolume = settingsData.musicVolume;
            fxVolume = settingsData.fxVolume;
        } else {
            quality = 0;
            brightnessValue = brightnessSlider.maxValue;
            musicVolume = 0.5f;
            fxVolume = 0.5f;
        }
    }

    private void ConfigurateUIElements() {
        qualityDropdown.value = quality;

        musicSlider.value = musicVolume;
        fXSlider.value = fxVolume;
        brightnessSlider.value = brightnessValue;
    } 

    public void RestoreDefaultValues() {
        SetQuality(quality);
        SetBrightness(brightnessValue);
        SetMusicVolume(musicVolume);
        SetFXVolume(fxVolume);
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetBrightness(float sliderValue) {
        float r = brightnessFilter.color.r;
        float g = brightnessFilter.color.g;
        float b = brightnessFilter.color.b;

        float alpha = Mathf.Abs(sliderValue);

        brightnessFilter.color = new Color(r, g, b, alpha);
    }

    public void SetMusicVolume(float sliderValue) {
        musicAudioMixer.SetFloat("Music Volume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetFXVolume(float sliderValue) {
        fXAudioMixer.SetFloat("FX Volume", Mathf.Log10(sliderValue) * 20);
    }

    public void SaveSettings() {
        quality = qualityDropdown.value;
        brightnessValue = brightnessSlider.value;
        musicVolume = musicSlider.value;
        fxVolume = fXSlider.value;

        GameManager.Instance.SaveSettings(this);
    }
}
