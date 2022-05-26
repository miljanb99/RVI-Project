using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private GameObject bossPrefab;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnBoss(bossPrefab));
    }

    private IEnumerator spawnBoss(GameObject enemy)
    {
        yield return new WaitForSeconds(2.0f);
        GameObject newBoss = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
    }
}
