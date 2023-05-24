using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer mixer, effectMixer;
    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;

    private void Start() {
        resolutions = Screen.resolutions;    

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        int i = 0;
        foreach (Resolution r in resolutions)
        {
            string option = r.width + " x " + r.height;

            options.Add(option);
            
            if( r.width == Screen.currentResolution.width && r.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

            i++;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolutions.Length;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("volume", volume);
    }

    public void SetEffectVolume(float volume)
    {
        effectMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];

        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }


}
