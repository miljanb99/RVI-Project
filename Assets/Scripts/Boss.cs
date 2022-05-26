using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private int damage = 5;

    [SerializeField]
    private float speed = 5f;

    private Controller2D controller;
    private GameObject player;

    private float gravity;
    private float jumpSpeed;
    Vector3 enemyVelocity;
    Vector2 enemyMoveTarget;
    private bool shouldJump;
    private bool flag = false;

    private BossHand bossHand;
    private BossHead bossHead;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<Controller2D>();
        SetBossValues();
        StartCoroutine(BossMovement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetBossValues()
    {
        GetComponent<Health>().SetHealth(50, 50);
        damage = 10;
        speed = 1.0f;
        gravity = player.GetComponent<PlayerController>().gravity;
        jumpSpeed = player.GetComponent<PlayerController>().jumpSpeed * 0.5f;
    }

    private IEnumerator BossMovement()
    {
        while (true)
        {
            Debug.Log(Mathf.Abs(waypoints[currentWaypointIndex].transform.position.x - transform.position.x));
            if(Mathf.Abs(waypoints[currentWaypointIndex].transform.position.x - transform.position.x) < .1f)
            {
                flag = !flag;
                currentWaypointIndex += 1;
                if(currentWaypointIndex >= 2)
                {
                    currentWaypointIndex = 0;
                }
            }
            enemyMoveTarget = new Vector2((waypoints[currentWaypointIndex].transform.position - transform.position).x, 0).normalized;
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
        if (collision.collider.CompareTag("Bullet"))
        {

            GetComponent<Health>().Damage(collision.collider.GetComponent<Bullet>().damage);
        }
    }
}
