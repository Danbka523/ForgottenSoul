using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuUI : MonoBehaviour
{
    //dropdown
    public TMP_Dropdown resolutionDropdown;
    private List<string> resolutions;

    //audio
    public AudioMixer audioMixer;

    //slider
    public Slider volumeSlider;


    //ui
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject help;
    private void Start()
    {
        int curr=0;
        resolutions = new List<string>();
        for (int i=0;i<Screen.resolutions.Length;i++)
        {
            string resStr = $"{Screen.resolutions[i].width}x{Screen.resolutions[i].height}x{Screen.resolutions[i].refreshRate}";
            resolutions.Add(resStr);
            if (Screen.resolutions[i].Equals(Screen.currentResolution))
                curr = i;
        }
        resolutionDropdown.AddOptions(resolutions);
        resolutionDropdown.value = curr;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void ResolutionChange()
    {
        int curr = resolutionDropdown.value;
        Screen.SetResolution(Screen.resolutions[curr].width,Screen.resolutions[curr].height,true,Screen.resolutions[curr].refreshRate);
    }

    public void VolumeChange()
    {
        if (volumeSlider.value == 0)
            audioMixer.SetFloat("Volume",-80f);
        else
            audioMixer.SetFloat("Volume", Mathf.Log10(volumeSlider.value)*30);
    }

    public void OnSettingsClick() {
        mainMenu.SetActive(false);
        settings.SetActive(true);
      
    }

    public void OnBackClick() {
        settings.SetActive(false);
        help.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnHelpClick() { 
          help.SetActive(true);
        mainMenu.SetActive(false);
    }
}
