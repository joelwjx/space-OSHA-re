using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    [SerializeField, Min(0)] private float minSpawnTime = 0;
    [SerializeField, Min(0)] private float maxSpawnTime = 10;
    [SerializeField] private float initialSpawnTime = 0;
    [SerializeField] private List<HazardSpawnPoint> spawnPoints;
    
    private float spawnTime = 0;
    private float spawnTimer = 0;

    private void OnValidate()
    {
        if(maxSpawnTime < minSpawnTime)
        {
            maxSpawnTime = minSpawnTime;
        }
    }

    private void Start()
    {
        spawnTime = initialSpawnTime;
    }

    private void Update()
    {
        if(spawnTimer > spawnTime)
        {
            spawnPoints[Random.Range(0, spawnPoints.Count)].Spawn();
            spawnTimer = 0;
            spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        }

        spawnTimer += Time.deltaTime;
    }
}
