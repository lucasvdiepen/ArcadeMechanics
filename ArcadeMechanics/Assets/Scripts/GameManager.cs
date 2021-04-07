using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public Health playerHealth;
    public Text scoreText;
    public Text highscoreText;
    public Canvas pauseScreen;

    private int score = 0;
    private int highscore = 0;
    public bool isPaused = false;

    void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        highscoreText.text = "Highscore: " + highscore;
    }

    void Update()
    {
        GetScore();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }    
    }

    private void Pause()
    {
        if(isPaused)
        {
            isPaused = false;
            pauseScreen.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            isPaused = true;
            pauseScreen.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void GetScore()
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
        FindObjectOfType<SoundmanagerScript>().PlayDeathSounds();
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
        playerHealth.ResetHealth();
        FindObjectOfType<BackgroundManager>().ResetBackground();
        FindObjectOfType<CloudsManager>().ResetClouds();
    }
}
