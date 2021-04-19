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
    public bool isPaused = false;
    public int speedIncreaseAt = 50;
    public float speedIncrease = 0.15f;

    public int maxSpeedIncrease = 10;

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
        highscoreText.text = "Highscore: " + FindObjectOfType<Leaderboard>().GetHighestScore();
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
            float newSpeed = Mathf.Clamp(score / speedIncreaseAt * speedIncrease, 0, maxSpeedIncrease);
            if(currentSpeed != newSpeed)
            {
                currentSpeed = newSpeed;
                playerMovement.speed = currentSpeed + playerMovement.startingSpeed;

                obstacleManager.minObstacleDistance = currentSpeed + obstacleManager.startingMinObstacleDistance;
                obstacleManager.maxObstacleDistance = currentSpeed + obstacleManager.startingMaxObstacleDistance;

                obstacleManager.minLongObstacleDistance = currentSpeed + obstacleManager.startingMinLongObstacleDistance;
                obstacleManager.maxLongObstacleDistance = currentSpeed + obstacleManager.startingMaxLongObstacleDistance;
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
        highscoreText.text = "Highscore: " + FindObjectOfType<Leaderboard>().GetHighestScore();

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
