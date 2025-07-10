using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnInfo
    {
        public GameObject spawnPrefab;
        public Transform spawnPos;
    }

    public List<SpawnInfo> infos = new List<SpawnInfo>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnAll();
    }

    void SpawnAll()
    {
        foreach (var info in infos)
        {
            Instantiate(info.spawnPrefab, info.spawnPos.position, Quaternion.identity);
        }
        
    }
}
