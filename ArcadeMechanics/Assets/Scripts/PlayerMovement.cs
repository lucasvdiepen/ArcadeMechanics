using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public GameManager gameManager;

    public float startingSpeed = 5f;
    [HideInInspector] public float speed = 5f;
    public float jumpForce = 5f;
    public float wallOffset = 1f;
    public bool playerRunAutomatic = false;
    public bool freezeMovement = false;

    public float speedUpTime = 1f;

    private float oldSpeed = 0;
    [HideInInspector] public bool speedingUp = false;

    private float timeElapsed = 0;

    public Vector3 startPosition;

    private Rigidbody2D rb;
    [HideInInspector] public bool grounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = startingSpeed;
        transform.position = startPosition;
    }

    void Update()
    {
        if(speedingUp)
        {
            speed = Mathf.Lerp(startingSpeed, oldSpeed, timeElapsed / speedUpTime);
            if (timeElapsed >= speedUpTime) speedingUp = false;

            Debug.Log(speed);

            timeElapsed += Time.deltaTime;
        }

        if(!gameManager.isPaused && !freezeMovement)
        {
            if (playerRunAutomatic)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);

                MoveBackroundAndClouds(1, speed);
            }
            else
            {
                //Player can be controlled

                int moveDirection = 0;

                Vector3 cameraLeftPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
                Vector3 cameraRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

                //Get movement input
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveDirection -= 1;
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveDirection += 1;

                bool canMove = true;

                if (transform.position.x <= cameraLeftPosition.x + wallOffset && moveDirection == -1) canMove = false;

                if (cameraMovement.freezeCameraMovement && transform.position.x >= cameraRightPosition.x - wallOffset && moveDirection == 1) canMove = false;

                if (canMove)
                {
                    //Move
                    if (moveDirection == -1)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        transform.Translate(moveDirection * speed * Time.deltaTime, 0, 0, Space.World);
                    }
                    else if (moveDirection == 1)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                        transform.Translate(moveDirection * speed * Time.deltaTime, 0, 0, Space.World);
                    }

                    //Move the background
                    MoveBackroundAndClouds(moveDirection, speed);

                }
            }

            //Jump
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                grounded = false;
                FindObjectOfType<SoundmanagerScript>().PlayJumpSounds();
            }
        }
    }

    public void StartBoss()
    {
        oldSpeed = speed;
        speed = startingSpeed;
    }

    public void StopBoss()
    {
        speedingUp = true;
    }

    private void MoveBackroundAndClouds(int moveDirection, float speed)
    {
        if (transform.position.x > Camera.main.transform.position.x && !FindObjectOfType<CameraMovement>().freezeCameraMovement)
        {
            FindObjectOfType<BackgroundManager>().MoveBackground(moveDirection, speed);
            FindObjectOfType<CloudsManager>().MoveClouds(moveDirection, speed);
        }
    }

    public void ResetPlayer()
    {
        speed = startingSpeed;
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        playerRunAutomatic = true;
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground") grounded = true;

        if (collision.transform.tag == "Obstacle")
        {
            gameManager.Die();
        }
    }
}   
