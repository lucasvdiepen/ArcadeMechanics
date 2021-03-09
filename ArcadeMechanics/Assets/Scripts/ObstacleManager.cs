using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstacles;

    public float minObstacleDistance = 6f;
    public float maxObstacleDistance = 15f;

    private float lastObstacleX = 0f;

    public float spawnObstacleOffset = 1f;

    private List<GameObject> activeObstacles = new List<GameObject>();

    void Start()
    {
        //SpawnObstacle();
    }

    void Update()
    {
        Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        if (cameraRightPosition.x >= lastObstacleX - spawnObstacleOffset)
        {
            SpawnObstacle();
        }
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
        int rndIndex = Random.Range(0, obstacles.Length);
        float rndDistance = Random.Range(minObstacleDistance, maxObstacleDistance);

        GameObject obstacle = Instantiate(obstacles[rndIndex]);
        obstacle.transform.position = new Vector3(lastObstacleX, obstacle.transform.position.y, obstacle.transform.position.z);
        obstacle.transform.Translate(new Vector3(rndDistance, 0, 0));
        lastObstacleX = obstacle.transform.position.x;
        activeObstacles.Add(obstacle);
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
