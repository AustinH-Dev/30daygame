using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] TMPro.TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;




    int ourAspect;

    void Start()
    {
        SetupResolutionDropdown();
    }

    private void SetupResolutionDropdown()
    {
        //Get an array of all of our resolutions
        resolutions = Screen.resolutions;
        //Clear the dropdowns of all resolutions
        resolutionDropdown.ClearOptions();

        //get a list to put our strings for each resolution in for each aspect
        List<string> resolutionStrings = new List<string>();

        int currentResolutionIndex = 0;
        int counter = 0;
        foreach (Resolution reso in resolutions)
        {
            string option = reso.width + "*" + reso.height + " " + reso.refreshRate + "hz";
            resolutionStrings.Add(option);

            if (reso.Equals(Screen.currentResolution))
            {
                //wow we need the index 1ce so we need this counter
                currentResolutionIndex = counter;
            }
            counter++;
        }

        //add the options and show our value
        resolutionDropdown.AddOptions(resolutionStrings);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void setResolution(int resoltuionIndex)
    {
        Resolution resolution = resolutions[resoltuionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolumeParam", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetDisplay(int displayIndex)
    {
        switch (displayIndex)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            default:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
        }
    }
}

