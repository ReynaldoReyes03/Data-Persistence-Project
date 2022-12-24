using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SettingsMenuUIHandler : MonoBehaviour {
    [Header("Audio Mixers")]
    [SerializeField] private AudioMixer musicAudioMixer;
    [SerializeField] private AudioMixer fXAudioMixer;

    [Header("Dropdowns")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Toggles")]
    [SerializeField] private Toggle isFullscreenToggle;

    [Header("Sliders")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider fXSlider;

    [Header("Containers")]
    [SerializeField] private GameObject resolutionContainer;
    [SerializeField] private GameObject fullscreenContainer;

    private GameObject brightnessPanel;
    private Image brightnessFilter;
    private SettingsData settingsData;

    [HideInInspector] public int resolution;
    [HideInInspector] public int quality;
    [HideInInspector] public bool isFullscreen;
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

        #if !UNITY_WEBGL || UNITY_EDITOR
            ConfigurateResolutionDropdown();
        #endif

        RestoreDefaultValues();
    }

    private void LoadDefaultSettings() {
        settingsData = GameManager.Instance.GetSettigns();

        if (settingsData != null) {
            #if !UNITY_WEBGL || UNITY_EDITOR
                resolution = settingsData.resolution;
                isFullscreen = settingsData.isFullscreen;
            #endif

            quality = settingsData.quality;
            brightnessValue = settingsData.brightnessValue;
            musicVolume = settingsData.musicVolume;
            fxVolume = settingsData.fxVolume;
        } else {
            #if !UNITY_WEBGL || UNITY_EDITOR
                resolution = 0;
                isFullscreen = false;
            #endif

            quality = 0;
            brightnessValue = brightnessSlider.maxValue;
            musicVolume = 0.5f;
            fxVolume = 0.5f;
        }
    }

    private void ConfigurateUIElements() {
        #if UNITY_WEBGL && !UNITY_EDITOR
            resolutionContainer.SetActive(false);
            fullscreenContainer.SetActive(false);
        #else
            resolutionDropdown.value = resolution;
            isFullscreenToggle.isOn = isFullscreen;
        #endif

        qualityDropdown.value = quality;

        musicSlider.value = musicVolume;
        fXSlider.value = fxVolume;
        brightnessSlider.value = brightnessValue;
    } 

    private void ConfigurateResolutionDropdown() {
        Resolution[] resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> resolutionDropdownOptions = new List<string>();

        for (int i = 0; i < resolutions.Length; i++) {
            string option = $"{resolutions[i].width} x {resolutions[i].height} @{resolutions[i].refreshRate}Hz";

            resolutionDropdownOptions.Add(option);

            if (GameManager.Instance.GetSettigns() == null) {
                bool width = resolutions[i].width == Screen.currentResolution.width;
                bool height = resolutions[i].height == Screen.currentResolution.height;
                bool refreshRate = resolutions[i].refreshRate == Screen.currentResolution.refreshRate;

                if (width && height && refreshRate) resolution = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionDropdownOptions);
        resolutionDropdown.value = resolution;
        resolutionDropdown.RefreshShownValue();
    }

    public void RestoreDefaultValues() {
        #if !UNITY_WEBGL || UNITY_EDITOR
            SetResolution(resolution);
            SetFullscreen(isFullscreen);
        #endif

        SetQuality(quality);
        SetBrightness(brightnessValue);
        SetMusicVolume(musicVolume);
        SetFXVolume(fxVolume);
    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution;

        if (resolutionIndex < Screen.resolutions.Length) {
            resolution = Screen.resolutions[resolutionIndex];
        } else {
            resolution = Screen.resolutions[0];
        }

        int width = resolution.width;
        int height = resolution.height;
        int refreshRate = resolution.refreshRate;

        Screen.SetResolution(width, height, Screen.fullScreen, refreshRate);
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
        
        #if UNITY_EDITOR
            EditorWindow window = EditorWindow.focusedWindow;
            window.maximized = isFullscreen;
        #endif
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
        #if !UNITY_WEBGL || UNITY_EDITOR
            resolution = resolutionDropdown.value;
            isFullscreen = isFullscreenToggle.isOn;
        #endif

        quality = qualityDropdown.value;
        brightnessValue = brightnessSlider.value;
        musicVolume = musicSlider.value;
        fxVolume = fXSlider.value;

        GameManager.Instance.SaveSettings(this);
    }

    public void ResetSettings() {
        GameManager.Instance.ResetSettings();
    }
}
