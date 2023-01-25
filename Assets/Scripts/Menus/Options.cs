using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public Slider volumeSlider;
    public TextMeshProUGUI resolutionDisplay;
    public Toggle fullscreenToggle;

    int resolutionIndex;

    public void Awake()
    {
        float volume = 0.5f;
        if (PlayerPrefs.HasKey(OptionKeys.Volume))
            volume = PlayerPrefs.GetFloat(OptionKeys.Volume);
        volumeSlider.value = volume;
        AudioListener.volume = volume;

        if (PlayerPrefs.HasKey(OptionKeys.Resolution))
        {
            resolutionIndex = PlayerPrefs.GetInt(OptionKeys.Resolution);
            SetResolution();
        }
        else
        {
            for (int i = 0; i < Screen.resolutions.Length; i++)
                if (Screen.resolutions[i].Equals(Screen.currentResolution))
                    resolutionIndex = i;
            SetResolutionDisplay();
        }

        bool fullScreen = true;
        if (PlayerPrefs.HasKey(OptionKeys.FullScreen))
            fullScreen = PlayerPrefs.GetInt(OptionKeys.FullScreen) > 0;
        fullscreenToggle.isOn = fullScreen;
        Screen.fullScreen = fullScreen;
    }

    public void AdjustVolume(float v)
    {
        AudioListener.volume = v;
        PlayerPrefs.SetFloat(OptionKeys.Volume, v);
    }

    public void SetFullScreen(bool v)
    {
        Screen.fullScreen = v;
        PlayerPrefs.SetInt(OptionKeys.FullScreen, v ? 1 : 0);
    }

    public void IncreaseResolution()
    {
        resolutionIndex = (resolutionIndex + 1) % Screen.resolutions.Length;
        SetResolution();
    }

    public void DecreaseResolution()
    {
        resolutionIndex--;
        if (resolutionIndex < 0)
            resolutionIndex = Screen.resolutions.Length + resolutionIndex;

        SetResolution();
    }

    void SetResolution()
    {
        if (resolutionIndex < 0)
            resolutionIndex = 0;
        else if (resolutionIndex >= Screen.resolutions.Length)
            resolutionIndex = Screen.resolutions.Length - 1;

        Resolution target = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(target.width, target.height, Screen.fullScreen, target.refreshRate);
        PlayerPrefs.SetInt(OptionKeys.Resolution, resolutionIndex);
        SetResolutionDisplay();
    }

    void SetResolutionDisplay() => resolutionDisplay.text = "" + Screen.resolutions[resolutionIndex];
}

public static class OptionKeys
{
    public static string Volume = "volume";
    public static string Resolution = "resolution";
    public static string FullScreen = "fullscreen";
}

public static class ResulutionExtensions
{
    public static bool Equals(this Resolution lhs, Resolution rhs)
    {
        return lhs.height == rhs.height
            && lhs.width == rhs.width
            && lhs.refreshRate == rhs.refreshRate;
    }
}
