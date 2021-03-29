using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI[] leaderboardScores;
    public TextMeshProUGUI[] leaderboardNames;
    public int highscore;

    /*
    public class Score
    {
        public int playerscore;
        public string playername;
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        leaderboardScores[0].text = "100";
        leaderboardNames[0].text = "LVD";

        leaderboardScores[1].text = "90";
        leaderboardNames[1].text = "DAN";

        leaderboardScores[2].text = "80";
        leaderboardNames[2].text = "BDB";

        leaderboardScores[3].text = "70";
        leaderboardNames[3].text = "SYM";

        leaderboardScores[4].text = "60";
        leaderboardNames[4].text = "KLK";

        leaderboardScores[5].text = "50";
        leaderboardNames[5].text = "SD1";

        leaderboardScores[6].text = "40";
        leaderboardNames[6].text = "SCH";

        leaderboardScores[7].text = "30";
        leaderboardNames[7].text = "FUN";

        leaderboardScores[8].text = "20";
        leaderboardNames[8].text = "USB";

        highscore = PlayerPrefs.GetInt("Highscore");
        int score1 = Int32.Parse(leaderboardScores[0].text);
        int score2 = Int32.Parse(leaderboardScores[1].text);
        int score3 = Int32.Parse(leaderboardScores[2].text);
        int score4 = Int32.Parse(leaderboardScores[3].text);
        int score5 = Int32.Parse(leaderboardScores[4].text);
        int score6 = Int32.Parse(leaderboardScores[5].text);
        int score7 = Int32.Parse(leaderboardScores[6].text);
        int score8 = Int32.Parse(leaderboardScores[7].text);
        int score9 = Int32.Parse(leaderboardScores[8].text);

        if (highscore >= score1)
        {
            highscore = score1;
            leaderboardScores[0].text = score1.ToString();
        }
        if (highscore <= score1 & highscore >= score2)
        {
            highscore = score2;
            leaderboardScores[1].text = score2.ToString();
        }
        if (highscore <= score2 & highscore >= score3)
        {
            highscore = score3;
            leaderboardScores[2].text = score3.ToString();
        }
        if (highscore <= score3 & highscore >= score4)
        {
            highscore = score4;
            leaderboardScores[3].text = score4.ToString();
        }
        if (highscore <= score4 & highscore >= score5)
        {
            highscore = score5;
            leaderboardScores[4].text = score5.ToString();
        }
        if (highscore <= score5 & highscore >= score6)
        {
            highscore = score6;
            leaderboardScores[5].text = score6.ToString();
        }
        if (highscore <= score6 & highscore >= score7)
        {
            highscore = score7;
            leaderboardScores[6].text = score7.ToString();
        }
        if (highscore <= score7 & highscore >= score8)
        {
            highscore = score8;
            leaderboardScores[7].text = score8.ToString();
        }
        if (highscore <= score8 & highscore >= score9)
        {
            highscore = score9;
            leaderboardScores[8].text = score9.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
