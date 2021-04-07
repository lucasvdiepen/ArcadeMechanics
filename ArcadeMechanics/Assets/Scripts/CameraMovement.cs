using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    public float cameraMoveToEnemyTime = 1f;
    public float camereMoveToPlayerTime = 0.5f;

    private float timeElapsed = 0;
    private bool moveSmooth = false;
    private float moveTime = 0;

    private float startPositionX;
    private float endPositionX;

    private float distanceToEgde = 0;

    [HideInInspector] public bool freezeCameraMovement = false;

    private GameObject player;

    private enum MovingTo
    {
        Enemy,
        Player
    }

    private MovingTo movingTo;

    void Start()
    {
        Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        distanceToEgde = cameraRightPosition.x - transform.position.x;
    }

    private void Update()
    {
        if(moveSmooth)
        {
            if (movingTo == MovingTo.Enemy) transform.position = new Vector3(Mathf.Lerp(startPositionX, endPositionX, timeElapsed / moveTime) - distanceToEgde, transform.position.y, transform.position.z);
            else if (movingTo == MovingTo.Player) transform.position = new Vector3(Mathf.Lerp(startPositionX, endPositionX, timeElapsed / moveTime), transform.position.y, transform.position.z);
            if (timeElapsed >= moveTime)
            {
                //Smooth camera move done
                if(movingTo == MovingTo.Enemy)
                {
                    moveSmooth = false;
                    FindObjectOfType<ObstacleManager>().SmoothCameraAnimationToObstacleDone();
                }
                else if(movingTo == MovingTo.Player)
                {
                    moveSmooth = false;
                    FindObjectOfType<ObstacleManager>().SmoothCameraAnimationToPlayerDone();
                }
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
        moveSmooth = false;
        timeElapsed = 0;
        moveTime = 0;
        SetCameraToTarget();
    }

    public void MoveSmoothToObstacle(float targetX)
    {
        FindObjectOfType<PlayerMovement>().freezeMovement = true;
        startPositionX = transform.position.x + distanceToEgde;
        endPositionX = targetX;
        timeElapsed = 0;
        moveTime = cameraMoveToEnemyTime;
        movingTo = MovingTo.Enemy;
        moveSmooth = true;
    }

    public void MoveSmoothToPlayer()
    {
        timeElapsed = 0;
        startPositionX = transform.position.x;
        endPositionX = target.position.x;
        moveTime = camereMoveToPlayerTime;
        movingTo = MovingTo.Player;
        moveSmooth = true;
    }
}
