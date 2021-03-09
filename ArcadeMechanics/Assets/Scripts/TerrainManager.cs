using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject startTerrain;
    public GameObject terrain;

    public float terrainOffset = 1f;

    private List<GameObject> terrains = new List<GameObject>();

    private Vector3 startPosition;

    void Start()
    {
        startPosition = startTerrain.transform.position;
        terrains.Add(startTerrain);
    }
    
    void Update()
    {
        Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        Vector3 cameraLeftPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));

        if(terrains.Count > 0)
        {
            //Add terrain
            Transform[] lastTerrainChilds = terrains[terrains.Count - 1].GetComponentsInChildren<Transform>();

            Transform lastTerrainEndPoint = GetEndPoint(lastTerrainChilds);

            if (cameraRightPosition.x >= lastTerrainEndPoint.position.x - terrainOffset)
            {
                GameObject newTerrain = Instantiate(terrain);
                newTerrain.transform.position = new Vector3(lastTerrainEndPoint.position.x, newTerrain.transform.position.y, newTerrain.transform.position.z);

                terrains.Add(newTerrain);

                //Generate obstacles in this terrain
                FindObjectOfType<ObstacleManager>().GenerateObstacles(newTerrain.transform.position);
            }

            //Remove terrain
            Transform[] firstTerrainChilds = terrains[0].GetComponentsInChildren<Transform>();

            Transform firstTerrainEndPoint = GetEndPoint(firstTerrainChilds);
          
            if (firstTerrainEndPoint.position.x + terrainOffset < cameraLeftPosition.x)
            {
                Destroy(terrains[0]);

                //Remove all obstacles in this terrain
                FindObjectOfType<ObstacleManager>().RemoveObstaclesBetween(terrains[0].transform.position.x, firstTerrainEndPoint.position.x);

                terrains.RemoveAt(0);
            }
        }
    }

    private Transform GetEndPoint(Transform[] childs)
    {
        foreach(Transform childTransform in childs)
        {
            if (childTransform.name == "EndPoint") return childTransform;
        }

        return null;
    }

    public void ResetTerrain()
    {
        for(int i = 0; i < terrains.Count; i++)
        {
            Destroy(terrains[i]);
        }

        terrains.Clear();

        GameObject startingTerrain = Instantiate(terrain);
        startingTerrain.transform.position = startPosition;

        terrains.Add(startingTerrain);
    }
}
