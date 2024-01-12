using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public int enemyCount;
    public int waveCount = 1;
    public GameObject enemySpawn;
    public float spawnRangeX = 10.0f;
    public float spawnRangeZ = 15.0f;
    public GameObject powerup;

    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveCount);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveCount++;
            SpawnEnemyWave(waveCount);
            Instantiate(powerup, PowerupSpawnPos(), powerup.transform.rotation);
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemy, RandomSpawnPos(), enemy.transform.rotation);
        }
    }

    Vector3 RandomSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        
        Vector3 spawnPos = new Vector3(spawnPosX, 0, 20);

        return spawnPos;
    }

    Vector3 PowerupSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        Vector3 powerupSpawnPos = new Vector3(spawnPosX, 2, spawnPosZ);

        return powerupSpawnPos;
    }
}
