using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float moveSpeed = 10f;
    public static float jumpHeight = 10f;

    public bool isGrounded = false;


    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();

         Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
         transform.position += movement * Time.deltaTime * moveSpeed;


    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true   )
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
         
    }

}
