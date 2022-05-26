using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHand : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 1.0f;

    private bool attacking = false;

    private void Start()
    {

       StartCoroutine(StartCountdown());
    }
    private void Update()
    {
        if(currCountdownValue == 0)
        {
            attacking = true;
        }

         if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
         {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        if (attacking)
        {
            StartCoroutine(StartAttack());
        }
        else
        {
            //transform.position = new Vector2((transform.position - waypoints[currentWaypointIndex].transform.position).x, 0).normalized;
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
           
        }
    }
    
    private float currCountdownValue;
    private IEnumerator StartCountdown(float countdownValue = 5)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
    }
    

    private IEnumerator StartAttack()
    {
        attacking = true;
        transform.Translate(new Vector3(0, -1.0f, 0) * Time.deltaTime * speed);
        yield return new WaitForSeconds(1.0f);
        transform.Translate(new Vector3(0, 1.0f, 0) * Time.deltaTime * speed);
        yield return new WaitForSeconds(0.5f);
        currCountdownValue = 1000;
        attacking = false;
        
    } 
}
