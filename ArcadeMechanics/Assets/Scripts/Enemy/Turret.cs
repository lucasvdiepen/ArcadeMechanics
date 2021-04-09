using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bullet;

    public float bulletSpeed = 10f;
    public float bulletSize = 1.568554f;

    public float burstDelayMillis = 100;

    [Range(0, 100)]
    public int burstChance = 75;

    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.CanAttack()) Attack();
    }

    private void Attack()
    {
        //Process how the enemy should attack
        if(Random.Range(0, 101) < burstChance)
        {
            StartCoroutine(burstAttack());
        }
        else
        {
            enemy.ShootBullet(bullet, bulletSpawnPoint, bulletSpeed, bulletSize);
        }
    }

    private IEnumerator burstAttack()
    {
        for(int i = 0; i < 3; i++)
        {
            enemy.ShootBullet(bullet, bulletSpawnPoint, bulletSpeed, bulletSize);
            yield return new WaitForSeconds(burstDelayMillis / 1000);
        }
    }
}
