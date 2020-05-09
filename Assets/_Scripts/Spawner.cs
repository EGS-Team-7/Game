using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float enemySpawnTimer;
    public GameObject[] enemies;

    public float terrainSpawnTimer;
    public GameObject[] terrain;

    public float spawnArea;

    float eTimer = .1f;
    float tTimer = .1f;

    void Start()
    {

    }


    void Update()
    {
        enemySpawnTimer -= Time.deltaTime / 300;
        terrainSpawnTimer -= Time.deltaTime / 300;

        eTimer -= Time.deltaTime;
        if(eTimer <= Time.deltaTime)
        {
            SpawnEnemy();
            eTimer = enemySpawnTimer;
        }

        tTimer -= Time.deltaTime;
        if(tTimer <= Time.deltaTime)
        {
            SpawnTerrain();
            tTimer = terrainSpawnTimer;
        }   
    }

    void SpawnEnemy()
    {
        float x = Random.Range(transform.position.x - spawnArea, transform.position.x + spawnArea);
        float y = Random.Range(transform.position.y - spawnArea, transform.position.y + spawnArea);

        if(x >= -2 && x <= 2)
        {
            x += Random.Range(3, 10);
        }
        if (y >= -2 && y <= 2)
        {
            y += Random.Range(3, 10);
        }

        int spawn = Random.Range(0, enemies.Length);

        Vector3 spawnPosition = new Vector3( x, y, 0);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(enemies[spawn], spawnPosition, spawnRotation);
    }

    void SpawnTerrain()
    {
        float x = Random.Range(transform.position.x - spawnArea, transform.position.x + spawnArea);
        float y = Random.Range(transform.position.y - spawnArea, transform.position.y + spawnArea);

        if (x >= -5 && x <= 5)
        {
            x += Random.Range(7, 15);
        }
        if (y >= -5 && y <= 5)
        {
            y += Random.Range(7, 15);
        }

        int spawn = Random.Range(0, terrain.Length);

        Vector3 spawnPosition = new Vector3(x, y, 0);
        Quaternion spawnRotation = Quaternion.identity;
        GameObject ter = Instantiate(terrain[spawn], spawnPosition, spawnRotation);
    }
}
