using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    public float cameraMoveToEnemyTime = 1f;

    private float timeElapsed = 0;
    private bool moveToEnemy = false;

    private float startPositionX;
    private float endPositionX;

    private float distanceToEgde = 0;

    [HideInInspector] public bool freezeCameraMovement = false;

    void Start()
    {
        Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        distanceToEgde = cameraRightPosition.x - transform.position.x;

        SetCameraToTarget();
    }

    private void Update()
    {
        if(moveToEnemy)
        {
            transform.position = new Vector3(Mathf.Lerp(startPositionX, endPositionX, timeElapsed / cameraMoveToEnemyTime) - distanceToEgde, transform.position.y, transform.position.z);
            if(timeElapsed >= cameraMoveToEnemyTime)
            {
                //Smooth camera move done
                moveToEnemy = false;
                FindObjectOfType<ObstacleManager>().SmoothCameraAnimationDone();
            }

            timeElapsed += Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        if(!freezeCameraMovement)
        {
            if(target.position.x > transform.position.x)
            {
                SetCameraToTarget();
            }
        }
    }

    private void SetCameraToTarget()
    {
        transform.position = new Vector3(target.position.x, 0, -10);
    }

    public void ResetCamera()
    {
        freezeCameraMovement = false;
        moveToEnemy = false;
        timeElapsed = 0;
        SetCameraToTarget();
    }

    public void MoveSmoothToEnemy(float target)
    {
        FindObjectOfType<PlayerMovement>().freezeMovement = true;
        startPositionX = transform.position.x + distanceToEgde;
        endPositionX = target;
        timeElapsed = 0;
        moveToEnemy = true;
    }
}
