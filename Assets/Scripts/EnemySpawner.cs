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

    [SerializeField] 
    public int maxBasicEnemies = 0;
    [SerializeField]
    public int maxFastEnemies = 0;
    [SerializeField]
    public int maxSlowEnemies = 0;


    int basicEnemiesSpawned = 0;
    int fastEnemiesSpawned = 0;
    int slowEnemiesSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        basicEnemiesSpawned = 0;
        fastEnemiesSpawned = 0;
        slowEnemiesSpawned = 0;
        StartCoroutine(spawnEnemy(basicEnemyInterval, basicEnemyPrefab, maxBasicEnemies, basicEnemiesSpawned));
        StartCoroutine(spawnEnemy(fastEnemyInterval, fastEnemyPrefab, maxFastEnemies, fastEnemiesSpawned));
        StartCoroutine(spawnEnemy(slowStrongEnemyInterval, slowStrongEnemyPrefab, maxSlowEnemies, slowEnemiesSpawned));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy, int maxEnemiesToSpawn, int enemiesSpawned)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (enemiesSpawned < maxEnemiesToSpawn)
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
