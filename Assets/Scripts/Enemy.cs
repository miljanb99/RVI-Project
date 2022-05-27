using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private EnemyData data;

    private GameObject player;

    private HealthBar healthBar;

    private Controller2D controller;
    
    private float gravity;
    private float jumpSpeed; 
    Vector3 enemyVelocity;
    Vector2 enemyMoveTarget;
    bool isFlying;
    bool shouldJump;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<Controller2D>();
        SetEnemyValues();

        StartCoroutine(EnemyMovement());
    }

    private void SetEnemyValues()
    {
        damage = data.damage;
        speed = data.speed;
        GetComponent<Health>().setHealth(data.hp);
        gravity = player.GetComponent<PlayerController>().gravity;
        jumpSpeed = player.GetComponent<PlayerController>().jumpSpeed;
    }

    private IEnumerator EnemyMovement()
    {
        while (true)
        {
            enemyMoveTarget = new Vector2((player.transform.position - transform.position).x, 0).normalized;
            yield return new WaitForSeconds(2);
        }
    }

    private void FixedUpdate()
    {
        enemyVelocity.x = enemyMoveTarget.x * speed;

        if (controller.collisionInfo.below || controller.collisionInfo.above)
        {
            enemyVelocity.y = 0;
        }
        if (shouldJump && controller.collisionInfo.below)
        {
            enemyVelocity.y = jumpSpeed;
        }
        enemyVelocity.y += gravity * Time.deltaTime;

        controller.Move(enemyVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Bullet"))
        {

            GetComponent<Health>().Damage(collision.collider.GetComponent<Bullet>().damage);
        }
    }
}
