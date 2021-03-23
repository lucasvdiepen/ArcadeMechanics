using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public Text[] leaderboardScores;
    // Start is called before the first frame update
    void Start()
    {
        leaderboardScores[0].text = "Scores:";
        leaderboardScores[1].text = "Names:";
        leaderboardScores[2].text = "100";
        leaderboardScores[3].text = "LVD";
        leaderboardScores[4].text = "90";
        leaderboardScores[5].text = "DAN";
        leaderboardScores[6].text = "80";
        leaderboardScores[7].text = "BDB";
        leaderboardScores[8].text = "70";
        leaderboardScores[9].text = "SYM";
        leaderboardScores[10].text = "60";
        leaderboardScores[11].text = "KLK";
        leaderboardScores[12].text = "50";
        leaderboardScores[13].text = "SD1";
        leaderboardScores[14].text = "40";
        leaderboardScores[15].text = "SCH";
        leaderboardScores[16].text = "30";
        leaderboardScores[17].text = "FUN";
        leaderboardScores[18].text = "20";
        leaderboardScores[19].text = "USB";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
