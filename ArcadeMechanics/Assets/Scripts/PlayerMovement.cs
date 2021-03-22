using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float startingSpeed = 5f;
    private float speed = 5f;
    public float jumpForce = 5f;
    public float wallOffset = 1f;
    public bool playerRunAutomatic = false;

    public Vector3 startPosition;

    private Rigidbody rb;
    public bool grounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        speed = startingSpeed;
        transform.position = startPosition;
    }

    void Update()
    {
        if(playerRunAutomatic)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);

            MoveBackround(1, speed);
        }
        else
        {
            //Player can be controlled

            int moveDirection = 0;

            Vector3 cameraLeftPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));

            //Get movement input
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveDirection -= 1;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveDirection += 1;

            if (transform.position.x > cameraLeftPosition.x + wallOffset || moveDirection == 1)
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
                MoveBackround(moveDirection, speed);
            }
        }
        

        //Jump
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
            FindObjectOfType<SoundmanagerScript>().PlayJumpSounds();
        }
    }

    private void MoveBackround(int moveDirection, float speed)
    {
        if (transform.position.x > Camera.main.transform.position.x)
        {
            FindObjectOfType<BackgroundManager>().MoveBackground(moveDirection, speed);
        }
    }

    public void ResetPlayer()
    {
        speed = startingSpeed;
        transform.position = startPosition;
        grounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground") grounded = true;

        if (collision.transform.tag == "Obstacle") FindObjectOfType<GameManager>().Die();
    }
}   
