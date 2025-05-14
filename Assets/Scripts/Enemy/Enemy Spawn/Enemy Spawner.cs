using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    public int spawnCount = 5;
    public float spawnRadius = 10f;
    bool isSpawn = false;
    List<GameObject> spawnList = new List<GameObject>();

    void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        spawnList.Clear();

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPos = GetRandomPosition();
            float randomY = Random.Range(0f, 360f);
            Quaternion randomRotation = Quaternion.Euler(0f, randomY, 0f);
            
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, randomRotation);
            spawnList.Add(enemy);

            Enemy enemyInstance = enemy.GetComponent<Enemy>();

            if(enemyInstance != null)
            {
                enemyInstance.spawner = this;
            }
        }
    }

    public void NotifyEnemyDead(GameObject enemy)
    {
        spawnList.Remove(enemy);

        if(spawnList.Count == 0)
        {
            StartCoroutine(SpawnTimer());
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = new Vector3(randomCircle.x, 0f, randomCircle.y);
        spawnPos += transform.position;
        return spawnPos;
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(5f);
        SpawnEnemies();
    }

}