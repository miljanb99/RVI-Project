using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 5f;
    private float speedMultiplier = 1;

    private GameObject player;
    private Vector3 movementTarget;
    private bool isAttacking = false;
    int damage;
    float cooldown = 1;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damage = FindObjectOfType<Boss>().GetComponent<Boss>().damage;
        movementTarget = waypoints[0].transform.position;
        //StartCoroutine(StartAttacking());
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown < 0 && Vector2.Distance(transform.position,player.transform.position) < transform.localScale.x)
        {
            cooldown = 1;
            player.GetComponent<PlayerController>().TakeDamage(damage);
        }

        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        if (!isAttacking) movementTarget = waypoints[currentWaypointIndex].transform.position;
        else
        {
            if (Vector2.Distance(movementTarget, transform.position) < .1f) EndAttack();
        }
        transform.position = Vector2.MoveTowards(transform.position, movementTarget, Time.deltaTime * speed * speedMultiplier);

    }
    

    public IEnumerator StartAttacking()
    {

        while(true)
        {
            yield return new WaitForSeconds(Random.Range(2.0f, 6.0f));
            Attack();
        }
        
    } 

    public void Attack()
    {
        speedMultiplier = 3;
        movementTarget = player.transform.position;
        isAttacking = true;
    }

    public void EndAttack()
    {
        speedMultiplier = 1;
        isAttacking = false;
    }

}
