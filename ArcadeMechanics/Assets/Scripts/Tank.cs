using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bullet;

    public float bulletSpeed = 10f;

    private int lookingDirection = -1;
    private float lastShootTime = 0;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        //if(cameraRightPosition.x > transform.position.x)
        if(enemy.canAttack)
        {
            float time = Time.time;
            if(time > (lastShootTime + enemy.attackDelay))
            {
                lastShootTime = time;
                Shoot();
            }    
        }
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        newBullet.GetComponent<Bullet>().StartBullet(lookingDirection, bulletSpeed);
    }
}
