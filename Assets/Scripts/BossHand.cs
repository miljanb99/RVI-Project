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


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movementTarget = waypoints[0].transform.position;
        //StartCoroutine(StartAttacking());
    }

    private void Update()
    {

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
        yield return new WaitForSeconds(Random.Range(2, 5));

        while(true)
        {
            Attack();
            yield return new WaitForSeconds(Random.Range(5, 10));
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
