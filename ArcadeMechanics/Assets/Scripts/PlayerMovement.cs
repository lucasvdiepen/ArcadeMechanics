using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        int moveDirection = 0;

        //Move
        //float horizontalMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //transform.Translate(horizontalMove, 0, 0);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveDirection -= 1;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveDirection += 1;

        if(moveDirection == -1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Translate(moveDirection * speed * Time.deltaTime, 0, 0, Space.World);
        }
        else if(moveDirection == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Translate(moveDirection * speed * Time.deltaTime, 0, 0, Space.World);
        }

        //Jump
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            grounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground") grounded = true;
    }
}
