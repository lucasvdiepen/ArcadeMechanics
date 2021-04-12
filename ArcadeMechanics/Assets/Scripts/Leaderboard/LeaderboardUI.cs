using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    public TextMeshProUGUI[] leaderboardScores;
    public TextMeshProUGUI[] leaderboardNames;

    private void Start()
    {
        UpdateLeaderboardOnScreen();
    }

    public void UpdateLeaderboardOnScreen()
    {
        Leaderboard.Score[] leaderboard = FindObjectOfType<Leaderboard>().GetLeaderboard();

        Debug.Log(leaderboard.Length);

        for (int i = 0; i < leaderboardScores.Length; i++)
        {
            leaderboardScores[i].text = leaderboard[i].score.ToString();
            leaderboardNames[i].text = leaderboard[i].name;
        }
    }
}
