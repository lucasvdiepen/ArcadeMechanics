using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject startTerrain;
    public GameObject[] terrains;

    public float terrainOffset = 1f;

    public int terrainGroup = 3;
    private int currentGroupCount = 0;

    private List<GameObject> activeTerrains = new List<GameObject>();

    private Vector3 startPosition;

    private int terrainIndex = 0;

    void Start()
    {
        startPosition = startTerrain.transform.position;
        activeTerrains.Add(startTerrain);
    }
    
    void Update()
    {
        Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        Vector3 cameraLeftPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));

        if(activeTerrains.Count > 0)
        {
            //Add terrain
            Transform lastTerrainEndPoint = GetEndPoint(activeTerrains[activeTerrains.Count - 1]);

            if (cameraRightPosition.x >= lastTerrainEndPoint.position.x - terrainOffset)
            {
                SpawnTerrain(lastTerrainEndPoint.position);
            }

            //Remove terrain
            Transform firstTerrainEndPoint = GetEndPoint(activeTerrains[0]);
          
            if (firstTerrainEndPoint.position.x + terrainOffset < cameraLeftPosition.x)
            {
                Destroy(activeTerrains[0]);

                //Remove all obstacles in this terrain
                FindObjectOfType<ObstacleManager>().RemoveObstaclesBetween(activeTerrains[0].transform.position.x, firstTerrainEndPoint.position.x);

                activeTerrains.RemoveAt(0);
            }
        }
    }

    public Vector3 PrepareForBoss()
    {
        Transform lastEndPoint = GetEndPoint(activeTerrains[activeTerrains.Count - 1]);

        Transform newEndPoint = SpawnTerrain(lastEndPoint.position);

        return newEndPoint.position;
    }

    private Transform SpawnTerrain(Vector3 lastTerrainEndPoint)
    {
        if (currentGroupCount > terrainGroup)
        {
            currentGroupCount = 0;
            terrainIndex = Random.Range(0, terrains.Length);
        }

        GameObject newTerrain = Instantiate(terrains[terrainIndex]);
        newTerrain.transform.position = new Vector3(lastTerrainEndPoint.x, newTerrain.transform.position.y, newTerrain.transform.position.z);

        activeTerrains.Add(newTerrain);

        currentGroupCount++;

        //Generate obstacles in this terrain
        //FindObjectOfType<ObstacleManager>().GenerateObstacles(newTerrain.transform.position);

        return GetEndPoint(newTerrain);
    }

    public static Transform GetEndPoint(GameObject terrain)
    {
        Transform[] children = terrain.GetComponentsInChildren<Transform>();

        foreach(Transform childTransform in children)
        {
            if (childTransform.name == "EndPoint") return childTransform;
        }

        return null;
    }

    public void ResetTerrain()
    {
        for(int i = 0; i < activeTerrains.Count; i++)
        {
            Destroy(activeTerrains[i]);
        }

        activeTerrains.Clear();

        GameObject startingTerrain = Instantiate(terrains[0]);
        startingTerrain.transform.position = startPosition;

        activeTerrains.Add(startingTerrain);
    }
}
