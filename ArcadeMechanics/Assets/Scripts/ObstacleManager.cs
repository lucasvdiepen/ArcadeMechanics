using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstacles;

    public GameObject[] bosses;

    public float minObstacleDistance = 6f;
    public float maxObstacleDistance = 15f;

    public int bossAfterObstacles = 50;

    private float lastObstacleX = 0f;

    public float spawnObstacleOffset = 1f;

    public float bossSmoothCameraStartAtDistance = 5f;
    public float bossCameraRightOffset = 1f;

    private bool shouldSpawn = true;

    private bool bossActive = false;

    private List<GameObject> activeObstacles = new List<GameObject>();

    private int spawnedObstacles = 0;

    private bool smoothCamera = false;

    void Start()
    {
        //SpawnObstacle();
    }

    void Update()
    {
        Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        Vector3 cameraLeftPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));

        if (cameraRightPosition.x >= lastObstacleX - spawnObstacleOffset)
        {
            SpawnObstacle();
        }

        if(spawnedObstacles % bossAfterObstacles == 0 && !bossActive)
        {
            SpawnBoss();
        }

        if(activeObstacles.Count > 0 && bossActive)
        {
            GameObject lastObstacle = activeObstacles[activeObstacles.Count - 1];

            if (cameraRightPosition.x >= lastObstacle.transform.position.x - bossSmoothCameraStartAtDistance && !smoothCamera)
            {
                smoothCamera = true;
                FindObjectOfType<CameraMovement>().MoveSmoothToEnemy(lastObstacle.transform.position.x + bossCameraRightOffset);
            }

            if(cameraLeftPosition.x >= lastObstacle.transform.position.x)
            {
                bossActive = false;
                smoothCamera = false;
                StartObstacleSpawn();
                FindObjectOfType<PlayerMovement>().playerRunAutomatic = true;
            }
        }
    }

    public void SmoothCameraAnimationDone()
    {
        GameObject lastObstacle = activeObstacles[activeObstacles.Count - 1];
        FindObjectOfType<PlayerMovement>().freezeMovement = false;
        FindObjectOfType<PlayerMovement>().playerRunAutomatic = false;
        lastObstacle.GetComponent<Enemy>().canAttack = true;
    }

    private void SpawnBoss()
    {
        StopObstacleSpawn();

        int rndIndex = Random.Range(0, bosses.Length);

        Vector3 bossSpawnPosition = FindObjectOfType<TerrainManager>().PrepareForBoss();

        GameObject newBoss = Instantiate(bosses[rndIndex]);
        newBoss.transform.position = new Vector3(bossSpawnPosition.x, newBoss.transform.position.y, newBoss.transform.position.z);

        activeObstacles.Add(newBoss);

        bossActive = true;
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
