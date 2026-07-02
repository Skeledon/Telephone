using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _optionsMainPanel;

    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private Slider _audioSlider;

    [SerializeField]
    private TMPro.TMP_Dropdown _resolutionDropdown;

    [SerializeField]
    private Toggle _fullScreenToggle;

    private OptionsManager _optionsManager;
    private void Awake()
    {
        /*Screen.SetResolution(_optionsData.Resolution.x, _optionsData.Resolution.y, _optionsData.FullScreen);

        float dB = Mathf.Log10(_optionsData.Volume) * 20f;
        _audioMixer.SetFloat("MasterVolume", dB);*/

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _optionsManager = FindAnyObjectByType<OptionsManager>();
        _resolutionDropdown.ClearOptions();
        TMPro.TMP_Dropdown.OptionData[] options = new TMPro.TMP_Dropdown.OptionData[Screen.resolutions.Length];
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            options[i] = new TMPro.TMP_Dropdown.OptionData(Screen.resolutions[i].width + "x" + Screen.resolutions[i].height);
            _resolutionDropdown.options.Add(options[i]);
        }
    }

    public void ChangeVolume()
    {
        _optionsManager.Settings.MasterVolume = _audioSlider.value;
        _optionsManager.ApplySettings();
    }

    public void ChangeResolution()
    {
        _optionsManager.Settings.ResolutionX = Screen.resolutions[_resolutionDropdown.value].width;
        _optionsManager.Settings.ResolutionY = Screen.resolutions[_resolutionDropdown.value].height;
        _optionsManager.ApplySettings();
    }

    public void ChangeFullScreen()
    {
        _optionsManager.Settings.FullScreen = _fullScreenToggle.isOn;
        _optionsManager.ApplySettings();
    }
    
    public void OpenMainPanel()
    {
        _audioSlider.value = _optionsManager.Settings.MasterVolume;
        _resolutionDropdown.value = Array.FindIndex(Screen.resolutions, r => r.width == _optionsManager.Settings.ResolutionX && r.height == _optionsManager.Settings.ResolutionY);
        _fullScreenToggle.isOn = _optionsManager.Settings.FullScreen;
        _optionsMainPanel.SetActive(true);
    }

    public void CloseMainPanel()
    {
        _optionsMainPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
