using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public GameObject[] backgrounds;
    public GameObject startingBackground;

    public float speedOffset = 0.5f;

    public float spawnBackgroundOffset = 1f;

    private List<GameObject> activeBackgrounds = new List<GameObject>();

    private void Start()
    {
        activeBackgrounds.Add(startingBackground);
    }

    private void Update()
    {
        if(activeBackgrounds.Count > 0)
        {
            Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

            Vector3 cameraLeftPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));

            Transform[] lastBackground = activeBackgrounds[activeBackgrounds.Count - 1].GetComponentsInChildren<Transform>();

            Transform lastBackgroundEndpoint = TerrainManager.GetEndPoint(lastBackground);

            if (lastBackgroundEndpoint.position.x < (cameraRightPosition.x + spawnBackgroundOffset))
            {
                //Spawn new background

                int rndIndex = Random.Range(0, backgrounds.Length);

                GameObject newBackground = Instantiate(backgrounds[rndIndex]);
                newBackground.transform.position = new Vector3(lastBackgroundEndpoint.position.x, newBackground.transform.position.y, newBackground.transform.position.z);

                activeBackgrounds.Add(newBackground);
            }

            Transform[] firstBackground = activeBackgrounds[0].GetComponentsInChildren<Transform>();

            if (TerrainManager.GetEndPoint(firstBackground).position.x + spawnBackgroundOffset < cameraLeftPosition.x)
            {
                //Remove first background
                Destroy(activeBackgrounds[0]);
                activeBackgrounds.RemoveAt(0);
            }
        }
    }

    public void MoveBackground(int moveDirection, float speed)
    {
        if(moveDirection == 1)
        {
            float backgroundSpeed = speed - speedOffset;

            foreach (GameObject background in activeBackgrounds)
            {
                background.transform.Translate(backgroundSpeed * Time.deltaTime, 0, 0, Space.World);
            }
        }
    }
}
