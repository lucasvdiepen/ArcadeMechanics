using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public Transform bulletSpawnPoint;

    public GameObject bullet;

    public float bulletForce = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //For testing
        if(Input.GetKeyDown(KeyCode.G))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);

        Rigidbody2D newBulletRB = newBullet.GetComponent<Rigidbody2D>();

        //newBulletRB.AddForce(transform.forward * bulletForce);
        newBulletRB.velocity = new Vector2(bulletForce, 0);

        Destroy(newBullet, 5f);
    }
}
