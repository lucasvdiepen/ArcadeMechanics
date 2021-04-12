using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    public float SFXSliderVolume;
    public float BGMSliderVolume;

    public Slider BGMSlider;
    public Slider SFXSlider;

    public Button backButton;

    public GameObject settingsCanvas;

    private static SettingsMenuScript settingsMenuScript;

    [HideInInspector] public bool settingsIsOpen = false;

    public void ShowSettings()
    {
        settingsIsOpen = true;
        settingsCanvas.SetActive(true);
    }

    private void BackButtonClicked()
    {
        settingsIsOpen = false;
        settingsCanvas.SetActive(false);
    }

    public void BGMChanged(float value)
    {
        Debug.Log("BGM Volume set to " + value);

    }
    public void SFXChanged(float value)
    {
        Debug.Log("SFX Volume set to " + value);
    }
    public void OnBGMSliderChanged()
    {
        BGMChanged(BGMSlider.value);
        BGMSliderVolume = BGMSlider.value;
        FindObjectOfType<SoundmanagerScript>().ChangeBGMVolume(BGMSliderVolume);
    }
    public void OnSFXSliderChanged()
    {
        SFXChanged(SFXSlider.value);
        SFXSliderVolume = SFXSlider.value;
        FindObjectOfType<SoundmanagerScript>().ChangeSFXVolume(SFXSliderVolume);
    }
    private void OnEnable()
    {
        BGMSlider.onValueChanged.AddListener(delegate { OnBGMSliderChanged(); });
        SFXSlider.onValueChanged.AddListener(delegate { OnSFXSliderChanged(); });

        backButton.onClick.AddListener(BackButtonClicked);
    }
    void OnDisable()
    {
        BGMSlider.onValueChanged.RemoveAllListeners();
        SFXSlider.onValueChanged.RemoveAllListeners();
    }
    private void Awake()
    {
        if(!settingsMenuScript)
        {
            DontDestroyOnLoad(gameObject);
            settingsMenuScript = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
