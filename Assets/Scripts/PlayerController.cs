using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    float horizontal;
    int speed = 5;
    int jumpCount = 0;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody2D>().velocity = new Vector3(horizontal * speed, GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 2)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200));
            jumpCount++;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        jumpCount = 0;
    }
}
