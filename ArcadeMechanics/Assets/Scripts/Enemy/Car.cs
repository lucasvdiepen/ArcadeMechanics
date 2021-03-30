using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Animator animator;
    public GameObject healthBar;

    private RectTransform healthBarTransform;

    public float speed = 5f;

    public float edgeOffset = 1f;

    public float takeDamageHeight = -2.16f;

    public int minGetDamage = 15;
    public int maxGetDamage = 25;

    private Enemy enemy;
    private Health health;

    private bool isPreparing = false;
    private bool move = false;
    private int moveDirection = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        health = GetComponent<Health>();
        healthBarTransform = healthBar.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (enemy.CanAttack()) Attack();
    }

    private void LateUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Car_Idle") && isPreparing)
        {
            isPreparing = false;
            move = true;
        }

        if (move)
        {
            //do move
            enemy.Move(moveDirection, speed);

            Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
            Vector3 cameraLeftPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));

            if (transform.position.x <= cameraLeftPosition.x + edgeOffset && moveDirection == -1) move = false;

            if (transform.position.x >= cameraRightPosition.x - edgeOffset && moveDirection == 1) move = false; 
        }
    }

    private void Attack()
    {
        //Process how the enemy should attack
        animator.SetTrigger("Prepare");
        isPreparing = true;

        Vector3 target = FindObjectOfType<PlayerMovement>().transform.position;

        if (transform.position.x < target.x) 
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            healthBarTransform.localRotation = Quaternion.Euler(180, 0, 0);
            moveDirection = 1;
        }

        if (transform.position.x > target.x) 
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            healthBarTransform.localRotation = Quaternion.Euler(180, 180, 0);
            moveDirection = -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if(move)
            {
                move = false;
                int rndDamage = Random.Range(enemy.minDamage, enemy.maxDamage + 1);
                collision.gameObject.GetComponent<Health>().TakeDamage(rndDamage);
            }
            else
            {
                int rndDamage = Random.Range(minGetDamage, maxGetDamage + 1);
                if (collision.transform.position.y >= takeDamageHeight) health.TakeDamage(rndDamage);
            }
        }
    }
}
