using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        transform.position = new Vector3(target.position.x, 0, -10);   
    }

    void LateUpdate()
    {
        if(target.position.x > transform.position.x)
        {
            transform.position = new Vector3(target.position.x, 0, -10);
        }
    }
}
