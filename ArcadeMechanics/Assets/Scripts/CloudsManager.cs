using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsManager : MonoBehaviour
{
    public GameObject[] clouds;

    public float speedOffset = 0.2f;

    public float minSpawnHeight = 2f;
    public float maxSpawnHeight = 5f;

    public float minRange = 8f;
    public float maxRange = 14f;

    public float removeOffset = 5f;

    private List<GameObject> activeClouds = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
        Vector3 cameraLeftPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));

        if(clouds.Length > 0)
        {
            //Add clouds
            float lastCloudX = 0f;

            if (activeClouds.Count > 0) lastCloudX = activeClouds[activeClouds.Count - 1].transform.position.x;

            if(cameraRightPosition.x >= lastCloudX)
            {
                SpawnCloud(lastCloudX);
            }

            //Remove clouds
            if(activeClouds.Count > 0)
            {
                if(cameraLeftPosition.x - removeOffset >= activeClouds[0].transform.position.x)
                {
                    Destroy(activeClouds[0]);
                    activeClouds.RemoveAt(0);
                }
            }
        }
    }

    public void MoveClouds(int moveDirection, float speed)
    {
        if (moveDirection == 1)
        {
            float cloudSpeed = speed - speedOffset;

            foreach (GameObject cloud in activeClouds)
            {
                cloud.transform.Translate(cloudSpeed * Time.deltaTime, 0, 0, Space.World);
            }
        }
    }

    private void SpawnCloud(float lastCloudX)
    {
        int rndIndex = Random.Range(0, clouds.Length);
        float rndHeight = Random.Range(minSpawnHeight, maxSpawnHeight);
        float rndRange = Random.Range(minRange, maxRange) + lastCloudX;

        GameObject newCloud = Instantiate(clouds[rndIndex]);
        newCloud.transform.position = new Vector3(rndRange, rndHeight, 1);

        activeClouds.Add(newCloud);
    }

    public void ResetClouds()
    {
        for(int i = 0; i < activeClouds.Count; i++)
        {
            Destroy(activeClouds[i]);
        }

        activeClouds.Clear();
    }
}
