using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletStartPoint;

    public float fireRate = 0.25f;
    public int startingBullets = 7;
    public float reloadTime = 3f;

    public int minDamage = 5;
    public int maxDamage = 15;

    public float bulletSpeed = 2f;

    public float bulletSize;

    public Vector3 gunScale;

    private int bullets = 0;
    private bool isReloading = false;
    private bool canShoot = true;
    private float lastShootTime = 0;

    private float timeElapsed = 0;

    private GameObject player;

    private void Start()
    {
        isReloading = false;

        transform.localScale = gunScale;

        bullets = startingBullets;

        FindObjectOfType<PlayerAttack>().UpdateAmmoText(bullets);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isReloading)
        {
            //Do rotation animation
            transform.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 360, timeElapsed / reloadTime));

            timeElapsed += Time.deltaTime;

            if (timeElapsed >= reloadTime)
            {
                //Reload
                bullets = startingBullets;
                FindObjectOfType<PlayerAttack>().UpdateAmmoText(bullets);
                //transform.localRotation = Quaternion.Euler(0, 0, 0);
                isReloading = false;
            }
        }
    }

    public void Shoot(int moveDirection)
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

                    ShootBullet(bullet, bulletStartPoint.transform, bulletSpeed, bulletSize, moveDirection);
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
    private void ShootBullet(GameObject bullet, Transform bulletSpawnPoint, float bulletSpeed, float size, int moveDirection)
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        newBullet.transform.localScale = new Vector3(size, size, size);
        Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        newBullet.GetComponent<Bullet>().StartBullet(moveDirection, bulletSpeed, Random.Range(minDamage, maxDamage + 1));
    }

}
