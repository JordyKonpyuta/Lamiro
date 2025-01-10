using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public GameObject settingsPanel;
    public Text volumeText;
    public Slider volumeSlider;
    public Toggle fullScreenToggle;
    public Dropdown qualityDropdown;

    public string levelToLoad;

    private void Start()
    {
        SetVolume();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void SettingsButton()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        volumeText.text = (volumeSlider.value * 100).ToString("0");
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void SetQuality()
    {
        PlayerPrefs.SetInt("masterQuality", qualityDropdown.value);
        QualitySettings.SetQualityLevel(qualityDropdown.value);
    }

    public void SetFullScreen()
    {
        PlayerPrefs.SetInt("masterFullscreen", fullScreenToggle.isOn ? 1 : 0);
        Screen.fullScreen = fullScreenToggle.isOn;
    }
}
