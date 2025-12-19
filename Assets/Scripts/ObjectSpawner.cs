using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{


    [SerializeField] private GameObject minSpawn;
    [SerializeField] private GameObject maxSpawn;


    [SerializeField] private int waveNumber;
    [SerializeField] private List<Wave> waves;


    [System.Serializable]
    public class Wave
    {
        public ObjectPooler objectPoller;
        public float spawnTimer;
        public float spawnInterval;
        public int objectPerWave;
        public int spawnerObjectCount;
    }


    void Start()
    {


    }

    void Update()
    {
        waves[waveNumber].spawnTimer -= Time.deltaTime * GameManager.Instance.worldSpeed;

        if (waves[waveNumber].spawnTimer <= 0)
        {
            waves[waveNumber].spawnTimer += waves[waveNumber].spawnInterval;
            SpaewnObject();
        }

        
        if (waves[waveNumber].spawnerObjectCount >= waves[waveNumber].objectPerWave)
        {
            waves[waveNumber].spawnerObjectCount = 0;
            waveNumber++;
            if (waveNumber == waves.Count) waveNumber = 0;
        }

    }

    private void SpaewnObject()
    {
        GameObject spawnedObject = waves[waveNumber].objectPoller.GetPoolObject();

        spawnedObject.transform.position = RandomSpawnPoint();
        spawnedObject.transform.rotation = transform.rotation;
        spawnedObject.SetActive(true);
        waves[waveNumber].spawnerObjectCount++;
    }


    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        spawnPoint.x = minSpawn.transform.position.x;
        spawnPoint.y = Random.Range(minSpawn.transform.position.y, maxSpawn.transform.position.y);

        return spawnPoint;

    }
}
