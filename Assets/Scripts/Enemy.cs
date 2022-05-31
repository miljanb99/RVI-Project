using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private EnemyData data;

    private GameObject player;

    private HealthBar healthBar;

    private Controller2D controller;


    private float gravity;
    private float jumpSpeed; 
    Vector3 enemyVelocity;
    Vector2 enemyMoveTarget;
    [SerializeField]
    bool isJumping;
    bool shouldJump;
    float cooldown = 1;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<Controller2D>();
        SetEnemyValues();

        StartCoroutine(EnemyMovement());
        StartCoroutine(EnemyJumping());
    }

    private void SetEnemyValues()
    {
        GetComponent<Health>().SetupMaxHP(data.hp);
        gravity = player.GetComponent<PlayerController>().gravity;
        jumpSpeed = player.GetComponent<PlayerController>().jumpSpeed;
    }

    private IEnumerator EnemyJumping()
    {
        if (isJumping)
        {
            while (true)
            {
                float randomNum = Random.Range(0.0f, 1.0f);
                if (randomNum < 0.2f)
                {
                    shouldJump = true;
                }
                yield return new WaitForSeconds(.5f);
            }
        }
        
    }

    private IEnumerator EnemyMovement()
    {
        while (true)
        {
            enemyMoveTarget = new Vector2((player.transform.position - transform.position).x, 0).normalized;
            yield return new WaitForSeconds(.5f);
        }
    }

    private void FixedUpdate()
    {

        cooldown -= Time.deltaTime;
        if(cooldown < 0 && Vector3.Distance(transform.position, player.transform.position) < transform.localScale.x * 1.1f)
        {
            cooldown = 1;
            player.GetComponent<PlayerController>().TakeDamage(data.damage);
        }
        enemyVelocity.x = enemyMoveTarget.x * data.speed;

        if (controller.collisionInfo.below || controller.collisionInfo.above)
        {
            enemyVelocity.y = 0;
        }
        if (shouldJump && controller.collisionInfo.below)
        {
            enemyVelocity.y = jumpSpeed;
            shouldJump = false;
        }
        enemyVelocity.y += gravity * Time.deltaTime;

        controller.Move(enemyVelocity * Time.deltaTime);
    }
 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            GetComponent<Health>().Damage(collision.collider.GetComponent<Bullet>().damage);
        }
    }

    
}
