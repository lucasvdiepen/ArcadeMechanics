using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinWorth = 1;

    public float coinDespawnOffset = 1f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
    }

    void Update()
    {
        Vector3 cameraLeftPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));

        if ((transform.position.x + coinDespawnOffset) < cameraLeftPosition.x)
        {
            Destroy(gameObject);
        }
    }

    public void PickCoin()
    {
        FindObjectOfType<GameManager>().AddCoins(coinWorth);
        Destroy(gameObject);
    }
}
