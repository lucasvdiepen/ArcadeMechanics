using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstacles;
    public float minObstacleDistance = 6f;
    public float maxObstacleDistance = 15f;

    public int minObstaclesSpawn = 1;
    public int maxObstacleSpawn = 2;

    private float lastObstacleX = 0f;

    private List<GameObject> activeObstacles = new List<GameObject>();

    void Start()
    {
        SpawnObstacle();
    }

    void Update()
    {
        
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

    public void GenerateObstacles(Vector3 terrainStart)
    {
        int objectsToSpawn = Random.Range(minObstaclesSpawn, maxObstacleSpawn);
        lastObstacleX = terrainStart.x;

        for(int i = 0; i < objectsToSpawn; i++)
        {
            activeObstacles.Add(SpawnObstacle());
        }
    }

    private GameObject SpawnObstacle()
    {
        int rndIndex = Random.Range(0, obstacles.Length);
        float rndDistance = Random.Range(minObstacleDistance, maxObstacleDistance);

        GameObject obstacle = Instantiate(obstacles[rndIndex]);
        obstacle.transform.position = new Vector3(lastObstacleX, obstacle.transform.position.y, obstacle.transform.position.z);
        obstacle.transform.Translate(new Vector3(rndDistance, 0, 0));
        lastObstacleX = obstacle.transform.position.x;
        return obstacle;
    }

    public void ResetObstacles()
    {
        for(int i = 0; i < activeObstacles.Count; i++)
        {
            Destroy(activeObstacles[i]);
        }

        activeObstacles.Clear();

        lastObstacleX = 0;
    }
}
