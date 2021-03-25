using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthBar;

    public int startingHealth = 100;
    public int health = 0;

    public bool isEnemy = false;

    private void Start()
    {
        health = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        healthBar.value = health;

        if(health <= 0)
        {
            //Dead
            if(!isEnemy)
            {
                FindObjectOfType<GameManager>().Die();
            }
            else
            {
                //Remove enemy
                Destroy(gameObject);
            }
        }
    }

    public void ResetHealth()
    {
        if(!isEnemy)
        {
            health = startingHealth;
            healthBar.value = health;
        }
    }
}
