using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject startTerrain;
    public GameObject terrain;

    public float terrainOffset = 1f;

    private List<GameObject> terrains = new List<GameObject>();

    void Start()
    {
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

            if(cameraRightPosition.x - terrainOffset >= lastTerrainChilds[1].position.x)
            {
                GameObject newTerrain = Instantiate(terrain);
                newTerrain.transform.position = new Vector3(lastTerrainChilds[1].position.x, newTerrain.transform.position.y, newTerrain.transform.position.z);

                terrains.Add(newTerrain);

                //Generate obstacles in this terrain
                FindObjectOfType<ObstacleManager>().GenerateObstacles(newTerrain.transform.position);
            }

            //Remove terrain
            Transform[] firstTerrainChilds = terrains[0].GetComponentsInChildren<Transform>();
          
            if (firstTerrainChilds[1].position.x + terrainOffset < cameraLeftPosition.x)
            {
                Destroy(terrains[0]);

                //Remove all obstacles in this terrain
                FindObjectOfType<ObstacleManager>().RemoveObstaclesBetween(terrains[0].transform.position.x, firstTerrainChilds[1].position.x);

                terrains.RemoveAt(0);
            }
        }
    }
}
