using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _optionsMainPanel;

    [SerializeField]
    private OptionsData _optionsData;

    [SerializeField]
    private AudioMixer _audioMixer;

    [SerializeField]
    private Slider _audioSlider;

    [SerializeField]
    private TMPro.TMP_Dropdown _resolutionDropdown;

    [SerializeField]
    private Toggle _fullScreenToggle;

    private void Awake()
    {
        Screen.SetResolution(_optionsData.Resolution.x, _optionsData.Resolution.y, _optionsData.FullScreen);

        float dB = Mathf.Log10(_optionsData.Volume) * 20f;
        _audioMixer.SetFloat("MasterVolume", dB);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _resolutionDropdown.ClearOptions();
        TMPro.TMP_Dropdown.OptionData[] options = new TMPro.TMP_Dropdown.OptionData[Screen.resolutions.Length];
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            options[i] = new TMPro.TMP_Dropdown.OptionData(Screen.resolutions[i].width + "x" + Screen.resolutions[i].height);
            _resolutionDropdown.options.Add(options[i]);
        }
        _audioSlider.value = _optionsData.Volume;
        _fullScreenToggle.isOn = _optionsData.FullScreen;
        _resolutionDropdown.value = Array.FindIndex(Screen.resolutions, r => r.width == _optionsData.Resolution.x && r.height == _optionsData.Resolution.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume()
    {
        float dB = Mathf.Log10(_audioSlider.value) * 20f;
        _audioMixer.SetFloat("MasterVolume", dB);
        _optionsData.Volume = _audioSlider.value;
    }

    public void ChangeResolution()
    {
        Screen.SetResolution(Screen.resolutions[_resolutionDropdown.value].width, Screen.resolutions[_resolutionDropdown.value].height, _fullScreenToggle);
        _optionsData.Resolution = new Vector2Int(Screen.resolutions[_resolutionDropdown.value].width, Screen.resolutions[_resolutionDropdown.value].height);
        _optionsData.FullScreen = _fullScreenToggle.isOn;
    }
    
    public void OpenMainPanel()
    {
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
