using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerController : MonoBehaviour
{
    public float jumpHeight = 4;
    public float timeToHighestJumpPoint = 0.4f;

    float moveSpeed = 6;
    [HideInInspector]
    public float jumpSpeed = 8;
    [HideInInspector]
    public float gravity = -20;

    Vector3 playerVelocity;
    Vector2 playerMoveInput;
    bool playerJumpInput;

    Controller2D controller;


    public GameManager m_gameManager;
    private int numberNumberOfKilledEnemies = 0;

    private void Start()
    {
        controller = GetComponent<Controller2D>();
        Instantiate(m_gameManager);

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToHighestJumpPoint,2);
        jumpSpeed = Mathf.Abs(gravity) * timeToHighestJumpPoint;
    }

    private void Update()
    {
        playerMoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerJumpInput = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        playerVelocity.x = playerMoveInput.x * moveSpeed;

        if (controller.collisionInfo.below || controller.collisionInfo.above)
        {
            playerVelocity.y = 0;
        }
        if(playerJumpInput && controller.collisionInfo.below)
        {
            playerVelocity.y = jumpSpeed;
        }
        playerVelocity.y += gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);
    }

    internal void TakeDamage(int damage)
    {
        GetComponent<Health>().Damage(damage);
    }

    public void updateNumberOfKilledEnemies()
    {
        numberNumberOfKilledEnemies += 1;
        Debug.Log(numberNumberOfKilledEnemies);
        if (numberNumberOfKilledEnemies == 5)
        {
            StartCoroutine(m_gameManager.DestroyDoor());
        }
    }

    public void bossDestroyed()
    {
        m_gameManager.Victory();
    }
}
