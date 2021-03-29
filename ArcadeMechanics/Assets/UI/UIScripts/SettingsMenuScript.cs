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
    public void BGMChanged(float value)
    {
        Debug.Log("BGM Volume set to " + value);
        FindObjectOfType<SoundmanagerScript>().ChangeBGMVolume(value);
    }
    public void SFXChanged(float value)
    {
        Debug.Log("SFX Volume set to " + value);
        FindObjectOfType<SoundmanagerScript>().ChangeSFXVolume(value);
    }
    public void OnBGMSliderChanged()
    {
        BGMChanged(BGMSlider.value);
        BGMSliderVolume = BGMSlider.value;
    }
    public void OnSFXSliderChanged()
    {
        SFXChanged(SFXSlider.value);
        SFXSliderVolume = SFXSlider.value;
    }
    private void OnEnable()
    {
        BGMSlider.onValueChanged.AddListener(delegate { OnBGMSliderChanged(); });
        SFXSlider.onValueChanged.AddListener(delegate { OnSFXSliderChanged(); });
    }
    void OnDisable()
    {
        BGMSlider.onValueChanged.RemoveAllListeners();
        SFXSlider.onValueChanged.RemoveAllListeners();
    }

}
