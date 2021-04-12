using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD

public class Health : MonoBehaviour
{
    public int startingHealth = 100;
    public int health = 0;

=======
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthBar;

    public int startingHealth = 100;
    public int health = 0;

    public bool isBoss = false;

>>>>>>> development
    private void Start()
    {
        health = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

<<<<<<< HEAD
        if(health <= 0)
        {
            //Dead
            FindObjectOfType<GameManager>().Die();
=======
        healthBar.value = health;

        if(health <= 0)
        {
            //Dead
            if(!isBoss)
            {
                FindObjectOfType<GameManager>().Die();
            }
            else
            {
                //Remove enemy
                FindObjectOfType<ObstacleManager>().BossKilled(transform.position);
                Destroy(gameObject);
            }
>>>>>>> development
        }
    }

    public void ResetHealth()
    {
<<<<<<< HEAD
        health = startingHealth;
=======
        if(!isBoss)
        {
            health = startingHealth;
            healthBar.value = health;
        }
>>>>>>> development
    }
}
