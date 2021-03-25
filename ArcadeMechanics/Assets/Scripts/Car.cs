using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Animator animator;

    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (enemy.CanAttack()) Attack();
    }

    private void Attack()
    {
        //Process how the enemy should attack
        
    }
}
