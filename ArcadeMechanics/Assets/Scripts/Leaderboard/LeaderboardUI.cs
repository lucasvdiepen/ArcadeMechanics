using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaderboardUI : MonoBehaviour
{
    public Button backButton;

    public TextMeshProUGUI[] leaderboardScores;
    public TextMeshProUGUI[] leaderboardNames;

    private void Start()
    {
        UpdateLeaderboardOnScreen();
    }

    public void UpdateLeaderboardOnScreen()
    {
        Leaderboard.Score[] leaderboard = FindObjectOfType<Leaderboard>().GetLeaderboard();

        for (int i = 0; i < leaderboardScores.Length; i++)
        {
            leaderboardScores[i].text = leaderboard[i].score.ToString();
            leaderboardNames[i].text = leaderboard[i].name;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitLeaderboard();
        }
    }

    private void BackButtonClicked()
    {
        ExitLeaderboard();
    }

    private void ExitLeaderboard()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void OnEnable()
    {
        backButton.onClick.AddListener(BackButtonClicked);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
    }
}
