using System;

[Serializable]
public class SettingsData {
    public int quality;
    public float brightnessValue;
    public float musicVolume;
    public float fxVolume;

    public SettingsData(SettingsMenuUIHandler settings) {
        quality = settings.quality;
        brightnessValue = settings.brightnessValue;
        musicVolume = settings.musicVolume;
        fxVolume = settings.fxVolume;
    }
}
