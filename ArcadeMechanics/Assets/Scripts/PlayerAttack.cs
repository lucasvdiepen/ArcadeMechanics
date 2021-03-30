using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject gunPosition;

    private GameObject gun;
    private Gun gunScript;

    private void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            gunScript.Shoot();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            gunScript.Reload();
        }
    }

    public void SetGun(GameObject newGun)
    {
        if(gun != null)
        {
            Destroy(gun);
            gun = null;
            gunScript = null;
        }

        gun = Instantiate(newGun, gunPosition.transform.position, Quaternion.identity);
        gunScript = newGun.GetComponent<Gun>();
    }
}
