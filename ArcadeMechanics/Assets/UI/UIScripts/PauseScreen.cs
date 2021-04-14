using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public Button settingsButton;
    public Button backButton;

    private void OnEnable()
    {
        settingsButton.onClick.AddListener(delegate { SettingsButtonClicked(); });
        backButton.onClick.AddListener(BackButtonClicked);
    }

    private void OnDisable()
    {
        settingsButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();
    }

    private void SettingsButtonClicked()
    {
        FindObjectOfType<SettingsMenuScript>().ShowSettings();
    }

    private void BackButtonClicked()
    {
        FindObjectOfType<GameManager>().Pause();
    }
}
