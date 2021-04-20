using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public Button settingsButton;
    public Button backButton;
    public Button mainMenuButton;

    private void OnEnable()
    {
        settingsButton.onClick.AddListener(delegate { SettingsButtonClicked(); });
        backButton.onClick.AddListener(BackButtonClicked);
        mainMenuButton.onClick.AddListener(MainMenuButtonClicked);
    }

    private void OnDisable()
    {
        settingsButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
    }

    private void MainMenuButtonClicked()
    {
        FindObjectOfType<GameManager>().Pause();
        SceneManager.LoadScene("MainScene");
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
