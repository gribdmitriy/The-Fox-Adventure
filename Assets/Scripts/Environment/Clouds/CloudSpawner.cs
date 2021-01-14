using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public float windspeed;
    public GameObject[] clouds;
    private Vector2[] spawnPoints;

    void Start()
    {

        spawnPoints = new Vector2[2];
        spawnPoints[0] = new Vector2(transform.position.x, transform.position.y + 1);
        spawnPoints[1] = new Vector2(transform.position.x, transform.position.y - 0.5f);
        SpawnCloud();

    }

    void SpawnCloud()
    {

        GameObject cloud = (GameObject) Instantiate (clouds[GetRandomSprite()]);
        cloud.transform.position = new Vector2(transform.position.x, Random.Range(spawnPoints[1].y, spawnPoints[0].y));

        ScheduleCloudSpawner();
    }

    void ScheduleCloudSpawner()
    {
        int nextSpawnTime = Random.Range(1, 4);
        Invoke("SpawnCloud", nextSpawnTime);
    }

    int GetRandomSprite()
    {
        return Random.Range(0, clouds.Length);
    }

    int GetRandomPoint()
    {
        return Random.Range(0, 4);
    }
}
