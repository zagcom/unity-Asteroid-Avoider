using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] asteroidPrefabs;
    [SerializeField] private float secondsBetweenAsteroids = 1.5f;
    [SerializeField] private Vector2 forceRange;

    private Camera mainCamera;
    private float timer;


    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnAsteroid();
            timer += secondsBetweenAsteroids;
                
        }
    }

    private void SpawnAsteroid()
    {
        int side = Random.Range(0, 4);

        Vector2 spawnPoind = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch (side)
        {
            case 0:
                //left
                spawnPoind.x = 0;
                spawnPoind.y = Random.value;
                direction = new Vector2(1f, Random.Range(-1f, 1f));
                break;
            case 1:
                //right
                spawnPoind.x = 1;
                spawnPoind.y = Random.value;
                direction = new Vector2(-1f, Random.Range(-1f, 1f));
                break;
            case 2:
                //bottom
                spawnPoind.x = Random.value;
                spawnPoind.y = 0;
                direction = new Vector2(Random.Range(-1f, 1f), 1f);
                break;
            case 3:
                //top
                spawnPoind.x = Random.value;
                spawnPoind.y = 1;
                direction = new Vector2(Random.Range(-1f, 1f), -1f);
                break;
        }

        Vector3 worldSpawnPoint =  mainCamera.ViewportToWorldPoint(spawnPoind);
        worldSpawnPoint.z = 0;
        GameObject selectedAsteroid = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];
        GameObject asteroidInstance = Instantiate(
            selectedAsteroid, 
            worldSpawnPoint, 
            Quaternion.Euler(0f,0f, Random.Range(0f, 360f)));

        Rigidbody rb = asteroidInstance.GetComponent<Rigidbody>();
        rb.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
    }
}
