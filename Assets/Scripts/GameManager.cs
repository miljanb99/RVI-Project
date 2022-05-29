using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController m_player;


    public GameObject m_door;
    public GameObject victoryScreen;

    public BossHand leftHand;
    public BossHand rightHand;
    
    public IEnumerator DestroyDoor()
    {
        m_door.GetComponent<SpriteRenderer>().enabled = false;
        while (true) {
            if (Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, m_door.transform.position) < 3.0f)
            {
                Object.Destroy(m_door);

                GameObject.FindGameObjectWithTag("MainCamera").transform.position = Vector3.Lerp(GameObject.FindGameObjectWithTag("MainCamera").transform.position, new Vector3(270.0f, 0, -10f), 0.1f);
                StartCoroutine(leftHand.StartAttacking());
                StartCoroutine(rightHand.StartAttacking());
                break;
            }
            else
            {
                yield return null;
            }
        }
    }

    public void Victory()
    {
        victoryScreen.SetActive(true);
        m_player.gameObject.SetActive(false);
    }
}
