using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public ObstacleManager obstacleManager;

    public Transform player;
    public Health playerHealth;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public Canvas pauseScreen;
    public GameObject gameOver;
    public GameObject nameInputCanvas;

    [HideInInspector] public int score = 0;
    private int highscore = 0;
    public bool isPaused = false;
    public int speedIncreaseAt = 50;
    public float speedIncrease = 0.15f;

    private float currentSpeed = 0;

    [HideInInspector] public int coins = 0;

    private bool isDead = false;

    public void AddCoins(int amount)
    {
        coins += amount;

        UpdateCoinText();
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        if (coins < 0) coins = 0;

        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        //Update coin text here
    }

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
            if(!nameInputCanvas.activeSelf)
            {
                if(FindObjectOfType<SettingsMenuScript>().settingsIsOpen)
                {
                    FindObjectOfType<SettingsMenuScript>().CloseSettings();
                }
                else
                {
                    if (FindObjectOfType<PlayerShop>().shopIsOpened)
                    {
                        FindObjectOfType<PlayerShop>().CloseShop();
                    }
                    else
                    {
                        Pause();
                    }
                }
            }
        }

        if(!obstacleManager.bossActive && !obstacleManager.shopActive && !playerMovement.speedingUp)
        {
            float newSpeed = score / speedIncreaseAt * speedIncrease;
            if(currentSpeed != newSpeed)
            {
                currentSpeed = newSpeed;
                playerMovement.speed = currentSpeed + playerMovement.startingSpeed;

                obstacleManager.minObstacleDistance = currentSpeed + obstacleManager.startingMinObstacleDistance;
                obstacleManager.maxObstacleDistance = currentSpeed + obstacleManager.startingMaxObstacleDistance;
            }
        }

        /*if(score % speedIncreaseAt == 0)
        {
            float multiplier = 1 + (score / speedIncreaseAt * speedIncrease);
            Debug.Log("Multiplier: " + multiplier);
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            playerMovement.speed = playerMovement.startingSpeed * multiplier;
            Debug.Log(playerMovement.speed);
        }*/
    }

    public void Pause()
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
        if(!isDead)
        {
            isDead = true;

            FindObjectOfType<SoundmanagerScript>().PlayDeathSounds();
            FindObjectOfType<PlayerMovement>().freezeMovement = true;
            gameOver.SetActive(true);

            if (score > FindObjectOfType<Leaderboard>().GetLowestScore())
            {
                nameInputCanvas.SetActive(true);
            }
        }
    }

    public void ResetGame()
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

        coins = 0;

        isDead = false;

        gameOver.SetActive(false);

        FindObjectOfType<TerrainManager>().ResetTerrain();
        FindObjectOfType<ObstacleManager>().ResetObstacles();
        FindObjectOfType<PlayerMovement>().ResetPlayerMovement();
        FindObjectOfType<PlayerAttack>().ResetPlayerAttack();
        FindObjectOfType<CameraMovement>().ResetCamera();
        playerHealth.ResetHealth();
        FindObjectOfType<BackgroundManager>().ResetBackground();
        FindObjectOfType<CloudsManager>().ResetClouds();
        FindObjectOfType<CoinsManager>().ResetCoins();
    }
}
