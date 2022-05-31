using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private int currentWaypointIndex = 0;

    [SerializeField]
    public int damage = 5;

    private bool flag = false;
    private GameObject player;

    private BossHand bossHandLeft;
    private BossHand bossHandRight;
    private BossHead bossHead;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

}
