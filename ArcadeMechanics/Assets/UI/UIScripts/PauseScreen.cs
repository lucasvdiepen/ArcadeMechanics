using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public Button settingsButton;

    private void OnEnable()
    {
        settingsButton.onClick.AddListener(delegate { SettingsButtonClicked(); });
    }

    private void OnDisable()
    {
        settingsButton.onClick.RemoveAllListeners();
    }

    private void SettingsButtonClicked()
    {
        FindObjectOfType<SettingsMenuScript>().ShowSettings();
    }
}
