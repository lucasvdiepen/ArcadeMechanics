using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenuScript : MonoBehaviour
{
    public float SFXSliderVolume;
    public float BGMSliderVolume;

    public Slider BGMSlider;
    public Slider SFXSlider;

    public TextMeshProUGUI BGMText;
    public TextMeshProUGUI SFXText;

    public Button backButton;

    public GameObject settingsCanvas;

    private static SettingsMenuScript settingsMenuScript;

    [HideInInspector] public bool settingsIsOpen = false;

    public void ShowSettings()
    {
        settingsIsOpen = true;
        settingsCanvas.SetActive(true);
    }
    private void Start()
    {
        float BGMSliderPrefs = PlayerPrefs.GetFloat("BGMSlider", 50);
        float SFXSliderPrefs = PlayerPrefs.GetFloat("SFXSlider", 50);
        BGMSlider.value = BGMSliderPrefs;
        SFXSlider.value = SFXSliderPrefs;
        SFXChanged(SFXSliderPrefs);
        BGMChanged(BGMSliderPrefs);
    }

    private void BackButtonClicked()
    {
        settingsIsOpen = false;
        settingsCanvas.SetActive(false);
    }

    public void BGMChanged(float value)
    {
        BGMSliderVolume = value;
        FindObjectOfType<SoundmanagerScript>().ChangeBGMVolume(BGMSliderVolume);
        BGMText.text = value + "%";
        PlayerPrefs.SetFloat("BGMSlider", value);
        PlayerPrefs.Save();
    }
    public void SFXChanged(float value)
    {
        SFXSliderVolume = value;
        FindObjectOfType<SoundmanagerScript>().ChangeSFXVolume(SFXSliderVolume);
        SFXText.text = value + "%";
        PlayerPrefs.SetFloat("SFXSlider", value);
        PlayerPrefs.Save();
    }
    public void OnBGMSliderChanged()
    {
        BGMChanged(BGMSlider.value);
        
    }
    public void OnSFXSliderChanged()
    {
        SFXChanged(SFXSlider.value);
        
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
