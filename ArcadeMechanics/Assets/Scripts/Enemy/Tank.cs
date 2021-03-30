using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bullet;

    public float bulletSpeed = 10f;
    public float bulletSize = 2.6f;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        if (enemy.CanAttack()) Attack();
    }

    private void Attack()
    {
        //Process how the enemy should attack
        enemy.ShootBullet(bullet, bulletSpawnPoint, bulletSpeed, bulletSize);
    }
}
