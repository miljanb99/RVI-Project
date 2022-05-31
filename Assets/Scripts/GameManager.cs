using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController m_player;
    Vector3 cameraDestination;

    public GameObject m_door;
    public GameObject victoryScreen;
    public Text victoryText;

    public BossHand leftHand;
    public BossHand rightHand;


    private void Start()
    {
        Time.timeScale = 1;
        cameraDestination = Camera.main.transform.position + new Vector3(25,0,0);
    }
    public IEnumerator DestroyDoor()
    {
        m_door.SetActive(false);
        while (true) {
            if (Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, m_door.transform.position) < 3.0f)
            {
                StartCoroutine(TransitionToBoss());
                break;
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator TransitionToBoss()
    {
        float currentTime = 0;
        float maxTime = 1;
        while(currentTime <= maxTime)
        {
            currentTime += Time.deltaTime;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraDestination, currentTime/maxTime);
            yield return null;
        }
        StartCoroutine(leftHand.StartAttacking());
        StartCoroutine(rightHand.StartAttacking());
    }

    public void Victory()
    {
        victoryScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Defeat()
    {
        victoryScreen.SetActive(true);
        victoryText.text = "You lost!";
        Time.timeScale = 0;
    }
}
