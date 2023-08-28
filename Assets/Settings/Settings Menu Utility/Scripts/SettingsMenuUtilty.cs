using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuUtilty : MonoBehaviour
{
    [Header("Main Menu")]
    [SerializeField] private bool hideMainMenu = false;
    [SerializeField] private Button settingsMenuButton;
    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject settingsMenuPrefab;

    [Header("Settings Menu")]
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Button exitSettingsMenuButton;

    private Resolution[] resolutions;
    private bool firstTimePlayed;

    private void Awake()
    {
        firstTimePlayed = PlayerPrefs.HasKey("firstTime");
        settingsMenuButton.onClick.AddListener(OpenSettingsMenu);
        exitSettingsMenuButton.onClick.AddListener(() => ExitSettingsMenu());
    }

    private void Start()
    {
        resolutions = Screen.resolutions;   

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        if (firstTimePlayed)
        {
            currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
        }

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + "      @" + resolutions[i].refreshRate + "Mz";
            options.Add(option);

            if (!firstTimePlayed)
            {
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                    PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
                }
            }
        }

        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        if (firstTimePlayed)
        {
            SetSettings();
        }
    }

    private void OpenSettingsMenu()
    {
        settingsMenuPrefab.SetActive(true);
        if (hideMainMenu == false) return;
        mainMenuObject.SetActive(false);
    }

    private void ExitSettingsMenu()
    {
        settingsMenuPrefab.SetActive(false);
        if (hideMainMenu == false) return;
        mainMenuObject.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("MasterVolume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("firstTime", 1);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityIndex", qualityIndex);
        PlayerPrefs.SetInt("firstTime", 1);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("isFullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.SetInt("firstTime", 1);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.SetInt("firstTime", 1);
    }

    private void SetSettings()
    {
        SetVolume(PlayerPrefs.GetFloat("Volume"));
        SetQuality(PlayerPrefs.GetInt("QualityIndex"));
        SetFullscreen(PlayerPrefs.GetInt("isFullscreen") == 1);
        SetResolution(PlayerPrefs.GetInt("ResolutionIndex"));

        qualityDropdown.value = PlayerPrefs.GetInt("QualityIndex");
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        fullscreenToggle.isOn = (PlayerPrefs.GetInt("isFullscreen") == 1);

        PlayerPrefs.SetInt("firstTime", 1);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("firstTime", 1);
    }
}
