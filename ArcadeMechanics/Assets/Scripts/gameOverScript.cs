using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOverScript : MonoBehaviour
{
    public Button restartButton;
    public Button mainMenuButton;
    public Button leaderBoardButton;
    public GameObject gameOverCanvas;

    private void leaderBoardButtonClicked()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    private void mainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }    

    private void restartButtonClicked()
    {
        FindObjectOfType<GameManager>().ResetGame();
    }

    private void OnEnable()
    {
        leaderBoardButton.onClick.AddListener(leaderBoardButtonClicked);
        mainMenuButton.onClick.AddListener(mainMenuButtonClicked);
        restartButton.onClick.AddListener(restartButtonClicked);
    }

    void OnDisable()
    {
        restartButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
        leaderBoardButton.onClick.RemoveAllListeners();
    }
}
