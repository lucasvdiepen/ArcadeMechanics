using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public GameObject coin;

    public float minXVelocity = -4f;
    public float maxXVelocity = 4f;

    public float minYVelocity = 3f;
    public float maxYVelocity = 5f;

    public void SpawnCoin(float x, float y, bool hasGravity, float velocityX = 0, float velocityY = 0)
    {
        GameObject newCoin = Instantiate(coin);
        newCoin.transform.position = new Vector3(x, y, newCoin.transform.position.z);
        Rigidbody2D rb = newCoin.GetComponent<Rigidbody2D>();
        if (!hasGravity) rb.gravityScale = 0;
        else rb.velocity = new Vector2(velocityX, velocityY);
    }

    public void CoinExplosion(float x, float y, int coinsToSpawn)
    {
        for(int i = 0; i < coinsToSpawn; i++)
        {
            SpawnCoin(x, y, true, Random.Range(minXVelocity, maxXVelocity), Random.Range(minYVelocity, maxYVelocity));
        }
    }
}
