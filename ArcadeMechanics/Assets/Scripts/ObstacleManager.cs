using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public Transform mainCamera;
    public Transform player;

    [Header("Obstacles")]

    public GameObject[] obstacles;

    public float minObstacleDistance = 6f;
    public float maxObstacleDistance = 15f;

    private float lastObstacleX = 0f;

    public float spawnObstacleOffset = 1f;

    public float obstacleSmoothCameraStartAtDistance = 5.3f;
    public float obstacleCameraRightOffset = 2f;

    private ObstacleType obstacleType;

    [Header("Bosses")]

    public GameObject[] obstacleBosses;
    public GameObject[] bosses;

    public int bossAfterObstacles = 50;
    public float bossKilledResumeOffset = 2f;

    [Range(0, 100)]
    public int obstacleBossChance = 50;

    private bool bossActive = false;
    private Vector3 bossLastPosition = Vector3.zero;
    private bool bossKilled = false;

    [Header("Shop")]

    public GameObject shop;
    public int shopAfterObstacles = 15;

    private bool shopActive = false;

    private List<GameObject> activeObstacles = new List<GameObject>();

    private bool shouldSpawn = true;

    private int spawnedObstacles = 0;

    private bool smoothCamera = false;

    private enum ObstacleType
    {
        ObstacleBoss,
        Boss,
        Shop
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
        if(spawnedObstacles % shopAfterObstacles == 0 && !shopActive && !bossActive)
        {
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
                    smoothCamera = true;
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
        if (cameraLeftPosition.x >= bossLastPosition.x + bossKilledResumeOffset && bossKilled && obstacleType == ObstacleType.Boss)
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
        FindObjectOfType<PlayerMovement>().playerRunAutomatic = true;
    }

    public void BossKilled(Vector3 lastPosition)
    {
        if (obstacleType == ObstacleType.Boss)
        {
            if (activeObstacles.Count > 0) activeObstacles.RemoveAt(activeObstacles.Count - 1);

            bossLastPosition = lastPosition;

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

        FindObjectOfType<PlayerMovement>().freezeMovement = false;
        FindObjectOfType<PlayerMovement>().playerRunAutomatic = false;
    }

    private void SpawnShop()
    {
        StopObstacleSpawn();

        obstacleType = ObstacleType.Shop;

        Vector3 shopSpawnPosition = FindObjectOfType<TerrainManager>().PrepareForObstacle();

        GameObject newShop = Instantiate(shop);
        newShop.transform.position = new Vector3(shopSpawnPosition.x, newShop.transform.position.y, newShop.transform.position.z);

        activeObstacles.Add(newShop);

        shopActive = true;
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
            float rndDistance = Random.Range(minObstacleDistance, maxObstacleDistance);

            GameObject obstacle = Instantiate(obstacles[rndIndex]);
            obstacle.transform.position = new Vector3(lastObstacleX, obstacle.transform.position.y, obstacle.transform.position.z);
            obstacle.transform.Translate(new Vector3(rndDistance, 0, 0));
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
        smoothCamera = false;

        lastObstacleX = 0;
    }
}
