using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController m_player;


    public GameObject m_door;
    public GameObject victoryScreen;

    public BossHand m_hand;
    
    public IEnumerator DestroyDoor()
    {
       
        while (true) {
            Debug.Log(Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, m_door.transform.position));
            if (Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, m_door.transform.position) < 3.0f)
            {
                Object.Destroy(m_door);

                GameObject.FindGameObjectWithTag("MainCamera").transform.position = Vector3.Lerp(GameObject.FindGameObjectWithTag("MainCamera").transform.position, new Vector3(270.0f, 0, -10f), 0.1f);
                StartCoroutine(m_hand.StartAttacking());
                break;
            }
            else
            {
                yield return new WaitForSeconds(1.0f);
            }
        }
    }

    public void Victory()
    {
        victoryScreen.SetActive(true);
        m_player.gameObject.SetActive(false);
    }
}
