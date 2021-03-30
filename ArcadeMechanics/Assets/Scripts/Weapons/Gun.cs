using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletStartPoint;

    public float fireRate = 0.25f;
    public int startingBullets = 7;
    public float reloadTime = 3f;

    private int bullets = 0;
    private bool isReloading = false;
    private bool canShoot = false;
    private float lastShootTime = 0;
    private float lastReloadTime = 0;

    private void Start()
    {
        bullets = startingBullets;
    }

    private void Update()
    {
        if(isReloading)
        {
            float time = Time.deltaTime;

            if (time > (lastReloadTime + reloadTime))
            {
                //Reload
                bullets = startingBullets;
                isReloading = false;
            }
        }
    }

    public void Shoot()
    {
        if(canShoot && !isReloading)
        {
            if(bullets > 0)
            {
                float time = Time.deltaTime;
                if (time >= (lastShootTime + fireRate))
                {
                    lastShootTime = time;

                    //Fire bullet here
                }
            }    
        }
    }

    public void Reload()
    {
        if(bullets != startingBullets)
        {
            lastReloadTime = Time.deltaTime;
            isReloading = true;
        }
    }
}
