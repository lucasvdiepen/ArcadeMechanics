using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth = 100;
    public int health = 0;

    private void Start()
    {
        health = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            //Dead
            FindObjectOfType<GameManager>().Die();
        }
    }

    public void ResetHealth()
    {
        health = startingHealth;
    }
}
