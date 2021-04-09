using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float destroyBulletTime = 5f;

    private int moveDiection = 0;
    private float speed = 0;
    private int damage = 0;

    private void Start()
    {
        Destroy(gameObject, destroyBulletTime);
    }

    private void Update()
    {
        transform.Translate(new Vector3(moveDiection * speed * Time.deltaTime, 0, 0), Space.World);
    }

    public void StartBullet(int _moveDirection, float _speed, int _damage)
    {
        moveDiection = _moveDirection;
        speed = _speed;
        damage = _damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player" || collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
