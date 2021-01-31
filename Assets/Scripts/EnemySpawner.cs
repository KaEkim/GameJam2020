using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public float spawnRadius = 5f;
    public int currEnemiesInSpawner = 0;
    public int maxPerSpawner = 50;
    public int currEnemies = 0;
    public int maxEnemies;
    public float spawnTimer = 5f;
    public float spawnTimerReset = 5f;
    public int spawnLevel = 1;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (spawnTimer <= 0) spawnTimer = 0;
        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0 && currEnemiesInSpawner < maxPerSpawner && currEnemies < maxEnemies)
        {
            spawnAnEnemy();
            spawnTimer = spawnTimerReset;
        }
    }

    private void spawnAnEnemy()
    {
        currEnemiesInSpawner++;
        Instantiate(enemyPrefab1, pickPointInRadius(), transform.rotation, this.transform);
    }

    public Vector3 pickPointInRadius()
    {
        Vector3 originPoint = transform.position;
        originPoint.x += Random.Range(-spawnRadius, spawnRadius);
        originPoint.y += 1f;
        originPoint.z += Random.Range(-spawnRadius, spawnRadius);
        return originPoint;
    }
}
