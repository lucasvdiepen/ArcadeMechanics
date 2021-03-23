using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI[] leaderboardScores;
    public TextMeshProUGUI[] leaderboardNames;
    // Start is called before the first frame update
    void Start()
    {
        leaderboardScores[0].text = "Scores:";
        leaderboardNames[0].text = "Names:";

        leaderboardScores[1].text = "100";
        leaderboardNames[1].text = "LVD";

        leaderboardScores[2].text = "90";
        leaderboardNames[2].text = "DAN";

        leaderboardScores[3].text = "80";
        leaderboardNames[3].text = "BDB";

        leaderboardScores[4].text = "70";
        leaderboardNames[4].text = "SYM";

        leaderboardScores[5].text = "60";
        leaderboardNames[5].text = "KLK";

        leaderboardScores[6].text = "50";
        leaderboardNames[6].text = "SD1";

        leaderboardScores[7].text = "40";
        leaderboardNames[7].text = "SCH";

        leaderboardScores[8].text = "30";
        leaderboardNames[8].text = "FUN";

        leaderboardScores[9].text = "20";
        leaderboardNames[9].text = "USB";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
