using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public Text scoreText;
    public Text highscoreText;

    public int score = 0;
    public int highscore = 0;

    void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = "Highscore: " + highscore;
    }

    void Update()
    {
        GetScore();
    }

    public void GetScore()
    {
        int currentScore = Mathf.RoundToInt(player.transform.position.x);

        if(currentScore > score)
        {
            score = currentScore;
            scoreText.text = "Score: " + score;
        }
    }

    public void Die()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        //Check if score is better than highscore
        if(score > highscore)
        {
            highscore = score;
            highscoreText.text = "Highscore: " + highscore;
            PlayerPrefs.SetInt("Highscore", score);
        }

        //reset game
        score = 0;
        scoreText.text = "Score: " + score;

        FindObjectOfType<TerrainManager>().ResetTerrain();
        FindObjectOfType<ObstacleManager>().ResetObstacles();
        FindObjectOfType<PlayerMovement>().ResetPlayer();
        FindObjectOfType<CameraMovement>().ResetCamera();
    }
}
