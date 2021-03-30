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

    [HideInInspector] public float minObstacleDistance = 6f;
    [HideInInspector] public float maxObstacleDistance = 15f;

    private float lastObstacleX = 0f;

    public float spawnObstacleOffset = 1f;

    [Header("Bosses")]

    public GameObject[] obstacleBosses;
    public GameObject[] bosses;

    public int bossAfterObstacles = 50;
    public float bossSmoothCameraStartAtDistance = 5f;
    public float bossCameraRightOffset = 1f;
    public float bossKilledResumeOffset = 2f;

    private bool bossActive = false;
    private BossType bossType;
    private Vector3 bossLastPosition = Vector3.zero;
    private bool bossKilled = false;

    private List<GameObject> activeObstacles = new List<GameObject>();

    private bool shouldSpawn = true;

    private int spawnedObstacles = 0;

    private bool smoothCamera = false;

    [Range(0, 100)]
    public int obstacleBossChance = 50;


    private enum BossType
    {
        ObstacleBoss,
        Boss
    }

    private void Start()
    {
        minObstacleDistance = startingMinObstacleDistance;
        maxObstacleDistance = startingMaxObstacleDistance;
    }

    void Update()
    {
        Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        Vector3 cameraLeftPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));

        //check if obstacles should be spawned
        if (cameraRightPosition.x >= lastObstacleX - spawnObstacleOffset)
        {
            SpawnObstacle();
        }

        //Check if boss should be spawned
        if(spawnedObstacles % bossAfterObstacles == 0 && !bossActive)
        {
            SpawnBoss();
        }

        if(activeObstacles.Count > 0 && bossActive)
        {
            GameObject lastObstacle = activeObstacles[activeObstacles.Count - 1];

            if (lastObstacle.transform.tag == "Enemy")
            {
                //Check when the camera should start to move smoothly to the enemy
                if (cameraRightPosition.x >= lastObstacle.transform.position.x - bossSmoothCameraStartAtDistance && !smoothCamera)
                {
                    smoothCamera = true;
                    FindObjectOfType<CameraMovement>().MoveSmoothToEnemy(lastObstacle.transform.position.x + bossCameraRightOffset);
                }

                //Check if player passed the obstacle boss
                if (cameraLeftPosition.x >= lastObstacle.transform.position.x && bossType == BossType.ObstacleBoss)
                {
                    BossDone();
                }
            }
        }

        //Check if player passed the boss last position
        if (cameraLeftPosition.x >= bossLastPosition.x + bossKilledResumeOffset && bossKilled && bossType == BossType.Boss)
        {
            BossDone();
        }
    }

    public void BossDone()
    {
        bossActive = false;
        smoothCamera = false;
        bossKilled = false;
        StartObstacleSpawn();
        FindObjectOfType<PlayerMovement>().playerRunAutomatic = true;
    }

    public void BossKilled(Vector3 lastPosition)
    {
        if (bossType == BossType.Boss)
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

    public void SmoothCameraAnimationToEnemyDone()
    {
        if(bossType == BossType.Boss) FindObjectOfType<CameraMovement>().freezeCameraMovement = true;

        GameObject lastObstacle = activeObstacles[activeObstacles.Count - 1];
        lastObstacle.GetComponent<Enemy>().canAttack = true;
        FindObjectOfType<PlayerMovement>().freezeMovement = false;
        FindObjectOfType<PlayerMovement>().playerRunAutomatic = false;
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
            bossType = BossType.ObstacleBoss;
        }
        else
        {
            int rndIndex = Random.Range(0, bosses.Length);
            bossToSpawn = bosses[rndIndex];
            bossType = BossType.Boss;
        }

        //Create new terrain for boss
        Vector3 bossSpawnPosition = FindObjectOfType<TerrainManager>().PrepareForBoss();

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
        smoothCamera = false;

        lastObstacleX = 0;
    }
}
