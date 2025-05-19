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
    public float spawnTime = 10f;
    public bool isSpawn = false;
    List<GameObject> enemyPool = new List<GameObject>();

    void Start()
    {
        InitializePool();
        SpawnEnemies();
    }

    void InitializePool()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.GetComponent<Enemy>().spawner = this;
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    public void SpawnEnemies()
    {
        foreach(GameObject enemy in enemyPool)
        {
            if(!enemy.activeInHierarchy) // if enemy setactive false?
            {
                Vector3 randomPos = GetRandomPosition();
                float randomY = Random.Range(0f, 360f);
                Quaternion randomRot = Quaternion.Euler(0, randomY, 0);

                enemy.transform.SetPositionAndRotation(randomPos, randomRot);
                enemy.SetActive(true);
            }
        }
    }

    public void NotifyEnemyDead(GameObject enemy)
    {
        if (!isSpawn)
        {
            Destroy(gameObject);
        }

        else
        {
            enemy.SetActive(false);

            if (IsEnemyAllDisable())
            {
                if (isSpawn)
                {
                    StartCoroutine(SpawnTimer());
                }
            }

        }
    }

    bool IsEnemyAllDisable()
    {
        foreach(GameObject enemy in enemyPool)
        {
            if(enemy.activeInHierarchy) return false;
        }
        return true;
    }

    private Vector3 GetRandomPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        return transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y); 
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnEnemies();
    }

}