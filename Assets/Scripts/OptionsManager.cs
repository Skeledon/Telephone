using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    [SerializeField]
    private SaveManager _saveManager;

    [SerializeField]
    private AudioMixer _audioMixer;

    [HideInInspector]
    public SaveManager.SettingsData Settings;



    public void Initialize()
    {
        Settings = _saveManager.LoadSettings();
        ApplySettings();
    }

    public void SaveSettings()
    {
        _saveManager.SaveSettings(Settings);
    }

    public void ApplySettings()
    {
        Screen.SetResolution(Settings.ResolutionX, Settings.ResolutionY, Settings.FullScreen);
        float dB = Mathf.Log10(Settings.MasterVolume) * 20f;
        _audioMixer.SetFloat("MasterVolume", dB);

        SaveSettings();
    }

}
