using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        SetCameraToTarget();
    }

    void LateUpdate()
    {
        if(target.position.x > transform.position.x)
        {
            SetCameraToTarget();
        }
    }

    private void SetCameraToTarget()
    {
        transform.position = new Vector3(target.position.x, 0, -10);
    }

    public void ResetCamera()
    {
        SetCameraToTarget();
    }
}
