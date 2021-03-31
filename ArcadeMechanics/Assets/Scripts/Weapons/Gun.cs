using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletStartPoint;

    public float fireRate = 0.25f;
    public int startingBullets = 7;
    public float reloadTime = 3f;

    public Vector3 gunScale;

    private int bullets = 0;
    private bool isReloading = false;
    private bool canShoot = true;
    private float lastShootTime = 0;

    private float timeElapsed = 0;

    private void Start()
    {
        isReloading = false;

        transform.localScale = gunScale;

        bullets = startingBullets;

        FindObjectOfType<PlayerAttack>().UpdateAmmoText(bullets);
    }

    private void Update()
    {
        if (isReloading)
        {
            //Do rotation animation
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 380, timeElapsed / reloadTime));

            timeElapsed += Time.deltaTime;

            if (timeElapsed >= reloadTime)
            {
                //Reload
                bullets = startingBullets;
                FindObjectOfType<PlayerAttack>().UpdateAmmoText(bullets);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
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
                float time = Time.time;
                if (time >= (lastShootTime + fireRate))
                {
                    lastShootTime = time;

                    bullets -= 1;

                    FindObjectOfType<PlayerAttack>().UpdateAmmoText(bullets);

                    //Fire bullet here
                    Debug.Log("Shoot bullet");
                }
            }    
        }
    }

    public void Reload()
    {
        if (bullets != startingBullets && !isReloading)
        {
            timeElapsed = 0;
            isReloading = true;
        }
    }
}
