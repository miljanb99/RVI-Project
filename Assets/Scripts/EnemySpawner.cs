using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private GameObject basicEnemyPrefab;

    [SerializeField]
    private GameObject fastEnemyPrefab;

    [SerializeField]
    private GameObject slowStrongEnemyPrefab;
    
    [SerializeField]
    private float basicEnemyInterval = 3.5f;

    [SerializeField]
    private float fastEnemyInterval = 6.5f;

    [SerializeField]
    private float slowStrongEnemyInterval = 10f;

    public int enemiesSpawned = 0;
    public int MAX_ENEMY_SPAWNED = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(basicEnemyInterval, basicEnemyPrefab));
        //StartCoroutine(spawnEnemy(fastEnemyInterval, fastEnemyPrefab));
        //StartCoroutine(spawnEnemy(slowStrongEnemyInterval, slowStrongEnemyPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (enemiesSpawned < MAX_ENEMY_SPAWNED)
            {
                int randSpawnPoint = Random.Range(0, spawnPoints.Length);

                GameObject newEnemy = Instantiate(enemy, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                enemiesSpawned += 1;
            }
            else
            {
                break;
            }
        }
     }

}
