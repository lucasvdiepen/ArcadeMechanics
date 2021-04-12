using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public int highscore;

    public int maxLeaderboardSpots = 10;

    [HideInInspector] public Score[] leaderboard;

    public static Leaderboard leaderboardInstance;
    
    public class Score
    {
        public int score;
        public string name;

        public Score(int _score, string _name)
        {
            score = _score;
            name = _name;
        }
    }

    private void Awake()
    {
        if (!leaderboardInstance)
        {
            DontDestroyOnLoad(gameObject);
            leaderboardInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Score[] GetLeaderboard()
    {
        return leaderboard;
    }

    public int GetLowestScore()
    {
        return leaderboard[maxLeaderboardSpots - 1].score;
    }

    public void UpdateLeaderboard(int newScore, string newName)
    {
        Score currentScore = new Score(newScore, newName);

        for(int i = 0; i < leaderboard.Length; i++)
        {
            if(currentScore.score > leaderboard[i].score)
            {
                //Remove last and move everything
                for (int j = maxLeaderboardSpots - 1; j >= i; j--)
                {
                    int tmpIndex = j - 1;
                    if(tmpIndex >= 0)
                    {
                        leaderboard[j] = new Score(0, "");
                        Score tmp = leaderboard[tmpIndex];
                        leaderboard[j] = tmp;
                    }
                }

                //Add new score
                leaderboard[i] = currentScore;

                break;
            }
        }

        SaveLeaderboard();
    }

    public void SaveLeaderboard()
    {
        for(int i = 0; i < leaderboard.Length; i++)
        {
            PlayerPrefs.SetInt("LeaderboardScore" + i, leaderboard[i].score);
            PlayerPrefs.SetString("LeaderboardName" + i, leaderboard[i].name);
        }

        PlayerPrefs.Save();
    }

    public void LoadLeaderboard()
    {
        leaderboard = new Score[maxLeaderboardSpots];

        for (int i = 0; i < leaderboard.Length; i++)
        {
            int score = PlayerPrefs.GetInt("LeaderboardScore" + i, 0);
            string name = PlayerPrefs.GetString("LeaderboardName" + i, "");

            leaderboard[i] = new Score(score, name);
        }
    }

    void Start()
    {
        LoadLeaderboard();
    }
}
