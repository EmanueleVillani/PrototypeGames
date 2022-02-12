using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLv2 : MonoBehaviour
{
    
   public static SpawnLv2 instance;

    [SerializeField]
    private GameObject enemy;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField]
    private float spawnWaitTime = 2f;

    [SerializeField]
    private int countEnemyToSpawn_min=20, countEnemyToSpawn_max=40;

    private int countEnemyToSpawn;

    private Transform playerPosition;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        playerPosition = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        StartCoroutine(_SpawnWave(spawnWaitTime));
    }

    void SpawnNewWaveOfEnemies()
    {
        if (spawnedEnemies.Count > 0)
            return;

        countEnemyToSpawn = Random.Range(countEnemyToSpawn_min, countEnemyToSpawn_max);

        for (int i = 0; i < countEnemyToSpawn ; i++)
        {
            Vector3 spawnPos = new Vector3(playerPosition.position.x + Random.Range(30f, 50f), playerPosition.position.y, playerPosition.position.z);

            GameObject newEnemy = Instantiate(enemy, spawnPos, transform.rotation);
            spawnedEnemies.Add(newEnemy);

        }
    }

    IEnumerator _SpawnWave(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SpawnNewWaveOfEnemies();
    }


    public void CheckToSpawnNewWave(GameObject EnemyToRemove)  
    {
        spawnedEnemies.Remove(EnemyToRemove);

        if (spawnedEnemies.Count == 0)
            StartCoroutine(_SpawnWave(spawnWaitTime));

    }
}
