using System;

[Serializable]
public class SettingsData {
    public int resolution;
    public int quality;
    public bool isFullscreen;
    public float brightnessValue;
    public float musicVolume;
    public float fxVolume;

    public SettingsData(SettingsMenuUIHandler settings) {
        resolution = settings.resolution;
        quality = settings.quality;
        isFullscreen = settings.isFullscreen;
        brightnessValue = settings.brightnessValue;
        musicVolume = settings.musicVolume;
        fxVolume = settings.fxVolume;
    }
}
