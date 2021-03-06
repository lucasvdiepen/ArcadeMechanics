using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public Transform mainCamera;
    public Transform player;

    [Header("Obstacles")]

    public GameObject[] obstacles;

    public float startingMinObstacleDistance = 6f;
    public float startingMaxObstacleDistance = 15f;

    public float startingMinLongObstacleDistance = 20f;
    public float startingMaxLongObstacleDistance = 25f;

    [HideInInspector] public float minObstacleDistance = 6f;
    [HideInInspector] public float maxObstacleDistance = 15f;

    [HideInInspector] public float minLongObstacleDistance = 20f;
    [HideInInspector] public float maxLongObstacleDistance = 25f;

    public int longObstacleChance = 15;

    private float lastObstacleX = 0f;

    public float spawnObstacleOffset = 1f;

    public float obstacleSmoothCameraStartAtDistance = 5.3f;
    public float obstacleCameraRightOffset = 2f;

    public int coinSpawnChance = 75;

    public float coinSpawnHeight = 1f;

    private ObstacleType obstacleType;

    [Header("Bosses")]

    public GameObject[] obstacleBosses;
    public GameObject[] bosses;

    public int bossAfterObstacles = 50;
    public float bossKilledResumeOffset = 2f;

    [HideInInspector] public bool bossActive = false;

    [Range(0, 100)]
    public int obstacleBossChance = 50;

    public int minBossKilledCoins = 18;
    public int maxBossKilledCoins = 25;

    private Vector3 bossLastPosition = Vector3.zero;
    private bool bossKilled = false;

    [Header("Shop")]

    public GameObject shop;
    public int shopAfterObstacles = 15;

    [HideInInspector] public bool shopActive = false;

    private bool shopPending = false;

    private List<GameObject> activeObstacles = new List<GameObject>();

    private bool shouldSpawn = true;

    private int spawnedObstacles = 0;

    private bool smoothCamera = false;

    public enum ObstacleType
    {
        ObstacleBoss,
        Boss,
        Shop
    }

    private void Start()
    {
        minObstacleDistance = startingMinObstacleDistance;
        maxObstacleDistance = startingMaxObstacleDistance;

        minLongObstacleDistance = startingMinLongObstacleDistance;
        maxLongObstacleDistance = startingMaxLongObstacleDistance;
    }

    void Update()
    {
        Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        Vector3 cameraLeftPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));

        //Check if obstacles should be spawned
        if (cameraRightPosition.x >= lastObstacleX - spawnObstacleOffset)
        {
            SpawnObstacle();
        }

        //Check if boss should be spawned
        if(spawnedObstacles % bossAfterObstacles == 0 && !bossActive)
        {
            SpawnBoss();
        }

        //Check if shop should be spawned
        if(spawnedObstacles % shopAfterObstacles == 0 && !shopActive && !shopPending)
        {
            SpawnShop();
        }

        //Check if a shop should be spawned when boss is done
        if(!bossActive && shopPending)
        {
            shopPending = false;
            SpawnShop();
        }

        if(activeObstacles.Count > 0 && (bossActive || shopActive))
        {
            GameObject lastObstacle = activeObstacles[activeObstacles.Count - 1];

            if (lastObstacle.transform.tag == "Enemy" || lastObstacle.transform.tag == "Shop")
            {
                //Check when the camera should start to move smoothly to the obstacle
                if (cameraRightPosition.x >= lastObstacle.transform.position.x - obstacleSmoothCameraStartAtDistance && !smoothCamera)
                {
                    if(activeObstacles.Count > 0)
                    {
                        for (int i = activeObstacles.Count - 1; i >= 0; i--)
                        {
                            //Should not destroy the enemy or shop
                            if (activeObstacles[i].transform.tag != "Enemy" && activeObstacles[i].transform.tag != "Shop")
                            {   
                                Destroy(activeObstacles[i]);
                                activeObstacles.RemoveAt(i);
                            }
                        }
                    }

                    smoothCamera = true;

                    //Set speed for player
                    FindObjectOfType<PlayerMovement>().StartObstacle();

                    //Starts hint
                    FindObjectOfType<HintManager>().StartObstacleHint(obstacleType);

                    //Starts smooth camera move
                    FindObjectOfType<CameraMovement>().MoveSmoothToObstacle(lastObstacle.transform.position.x + obstacleCameraRightOffset);
                }

                //Check if player passed the obstacle
                if (cameraLeftPosition.x >= lastObstacle.transform.position.x && (obstacleType == ObstacleType.ObstacleBoss || obstacleType == ObstacleType.Shop))
                {
                    ObstacleDone();
                }
            }
        }

        //Check if player passed the boss last position
        if (cameraLeftPosition.x >= bossLastPosition.x + bossKilledResumeOffset && bossKilled && (obstacleType == ObstacleType.Boss || obstacleType == ObstacleType.ObstacleBoss))
        {
            ObstacleDone();
        }
    }

    public void ObstacleDone()
    {
        if (obstacleType == ObstacleType.Shop) FindObjectOfType<PlayerAttack>().shootingAllowed = true;

        bossActive = false;
        shopActive = false;
        smoothCamera = false;
        bossKilled = false;
        StartObstacleSpawn();
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.playerRunAutomatic = true;
        playerMovement.StopObstacle();
        
    }

    public void BossKilled(Vector3 lastPosition)
    {
        if (obstacleType == ObstacleType.Boss || obstacleType == ObstacleType.ObstacleBoss)
        {
            if (activeObstacles.Count > 0) activeObstacles.RemoveAt(activeObstacles.Count - 1);

            bossLastPosition = lastPosition;

            FindObjectOfType<CoinsManager>().CoinExplosion(bossLastPosition.x, bossLastPosition.y, Random.Range(minBossKilledCoins, maxBossKilledCoins + 1));

            //if player past the camera center the camera should smoothly move to the player
            if (player.position.x > mainCamera.position.x) FindObjectOfType<CameraMovement>().MoveSmoothToPlayer();
            else SmoothCameraAnimationToPlayerDone();
        }
    }

    public void SmoothCameraAnimationToPlayerDone()
    {
        FindObjectOfType<CameraMovement>().freezeCameraMovement = false;
        bossKilled = true;
    }

    public void SmoothCameraAnimationToObstacleDone()
    {
        if(obstacleType == ObstacleType.Boss) FindObjectOfType<CameraMovement>().freezeCameraMovement = true;

        if (obstacleType == ObstacleType.Shop) FindObjectOfType<PlayerAttack>().shootingAllowed = false;

        if(obstacleType == ObstacleType.Boss || obstacleType == ObstacleType.ObstacleBoss)
        {
            GameObject lastObstacle = activeObstacles[activeObstacles.Count - 1];
            lastObstacle.GetComponent<Enemy>().canAttack = true;
        }

        if(activeObstacles.Count > 0)
        {
            GameObject lastObstacle = activeObstacles[activeObstacles.Count - 1];
            if(lastObstacle.transform.tag == "Enemy")
            {
                lastObstacle.GetComponent<Enemy>().canAttack = true;
            }
        }

        FindObjectOfType<PlayerMovement>().freezeMovement = false;
        FindObjectOfType<PlayerMovement>().playerRunAutomatic = false;
    }

    private void SpawnShop()
    {
        if (!bossActive)
        {
            StopObstacleSpawn();

            obstacleType = ObstacleType.Shop;

            Vector3 shopSpawnPosition = FindObjectOfType<TerrainManager>().PrepareForObstacle();

            GameObject newShop = Instantiate(shop);
            newShop.transform.position = new Vector3(shopSpawnPosition.x, newShop.transform.position.y, newShop.transform.position.z);

            activeObstacles.Add(newShop);

            shopActive = true;
        }
        else shopPending = true;
    }

    private void SpawnBoss()
    {
        StopObstacleSpawn();

        //Choose between obstacleBoss and normal boss
        GameObject bossToSpawn;

        if(Random.Range(0, 101) < obstacleBossChance)
        {
            int rndIndex = Random.Range(0, obstacleBosses.Length);
            bossToSpawn = obstacleBosses[rndIndex];
            obstacleType = ObstacleType.ObstacleBoss;
        }
        else
        {
            int rndIndex = Random.Range(0, bosses.Length);
            bossToSpawn = bosses[rndIndex];
            obstacleType = ObstacleType.Boss;
        }

        //Create new terrain for boss
        Vector3 bossSpawnPosition = FindObjectOfType<TerrainManager>().PrepareForObstacle();

        //Spawn new boss
        GameObject newBoss = Instantiate(bossToSpawn);
        newBoss.transform.position = new Vector3(bossSpawnPosition.x, newBoss.transform.position.y, newBoss.transform.position.z);

        activeObstacles.Add(newBoss);

        bossActive = true;
        bossKilled = false;
    }    

    public void RemoveObstaclesBetween(float startX, float endX)
    {
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            float obstacleX = activeObstacles[i].transform.position.x;
            if(obstacleX >= startX && obstacleX <= endX)
            {
                Destroy(activeObstacles[i]);
                activeObstacles.RemoveAt(i);
            }
        }
    }

    private void SpawnObstacle()
    {
        if(shouldSpawn)
        {
            int rndIndex = Random.Range(0, obstacles.Length);

            float rndDistance = 0;

            if(Random.Range(0, 101) <= longObstacleChance)
            {
                rndDistance = Random.Range(minLongObstacleDistance, maxLongObstacleDistance);
                Debug.Log("Long obstacle");
            }
            else
            {
                rndDistance = Random.Range(minObstacleDistance, maxObstacleDistance);
            }

            GameObject obstacle = Instantiate(obstacles[rndIndex]);
            obstacle.transform.position = new Vector3(lastObstacleX, obstacle.transform.position.y, obstacle.transform.position.z);
            obstacle.transform.Translate(new Vector3(rndDistance, 0, 0));

            //Spawn coin above obstacle
            if(Random.Range(0, 101) <= coinSpawnChance)
            {
                FindObjectOfType<CoinsManager>().SpawnCoin(obstacle.transform.position.x, obstacle.transform.position.y + coinSpawnHeight, false);
            }

            lastObstacleX = obstacle.transform.position.x;
            activeObstacles.Add(obstacle);
            spawnedObstacles++;
        }
    }

    public void StopObstacleSpawn()
    {
        shouldSpawn = false;
    }

    public void StartObstacleSpawn()
    {
        Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        lastObstacleX = cameraRightPosition.x;
        shouldSpawn = true;
    }

    public void ResetObstacles()
    {
        for(int i = 0; i < activeObstacles.Count; i++)
        {
            Destroy(activeObstacles[i]);
        }

        activeObstacles.Clear();

        spawnedObstacles = 0;

        shouldSpawn = true;
        bossActive = false;
        shopActive = false;
        shopPending = false;
        smoothCamera = false;

        lastObstacleX = 0;
    }
}
