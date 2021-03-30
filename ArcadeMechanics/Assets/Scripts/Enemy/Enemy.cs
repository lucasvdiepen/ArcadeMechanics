using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackDelay = 1f;

    public int minDamage = 5;
    public int maxDamage = 15;

    [HideInInspector] public bool canAttack = false;

    private float lastAttackTime = 0;

    private int lookingDirection = -1;

    public bool CanAttack()
    {
        if (canAttack)
        {
            float time = Time.time;
            if (time > (lastAttackTime + attackDelay))
            {
                lastAttackTime = time;
                return true;
            }
        }

        return false;
    }

    public void Move(int moveDirection, float speed)
    {
        lookingDirection = moveDirection;
        transform.Translate(moveDirection * speed * Time.deltaTime, 0, 0, Space.World);
    }

    public void ShootBullet(GameObject bullet, Transform bulletSpawnPoint, float bulletSpeed, float size)
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        newBullet.transform.localScale = new Vector3(size, size, size);
        Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        newBullet.GetComponent<Bullet>().StartBullet(lookingDirection, bulletSpeed, Random.Range(minDamage, maxDamage + 1));
    }
}
