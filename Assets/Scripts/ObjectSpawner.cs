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
        public GameObject prefab;
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

        if (waves[waveNumber].spawnerObjectCount >= waves[waveNumber].objectPerWave)
        {
            waves[waveNumber].spawnerObjectCount = 0;
            waveNumber++;
            if (waveNumber == waves.Count) waveNumber = 0;
        }

        waves[waveNumber].spawnTimer += Time.deltaTime * PlayerController.Instance.boost;

        if (waves[waveNumber].spawnTimer >= waves[waveNumber].spawnInterval)
        {
            waves[waveNumber].spawnTimer = 0;
            SpaewnObject();
            waves[waveNumber].spawnerObjectCount++;

        }






    }

    private void SpaewnObject()
    {
        Instantiate(waves[waveNumber].prefab, RandomSpawnPoint(), transform.rotation);
    }


    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint;

        spawnPoint.x = minSpawn.transform.position.x;
        spawnPoint.y = Random.Range(minSpawn.transform.position.y, maxSpawn.transform.position.y);

        return spawnPoint;

    }
}
