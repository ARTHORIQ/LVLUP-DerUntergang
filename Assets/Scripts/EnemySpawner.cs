using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool enemyPool; // The object pool to use for spawning enemies.
    public float spawnDelay = 5f; // The delay between spawns.
    public int maxSpawns = 5; // The maximum number of enemies to spawn.

    private int numSpawns = 0; // The number of enemies spawned so far.

    void Start()
    {
        // Start spawning enemies using a coroutine.
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        // Keep spawning enemies until the maximum number is reached.
        while (numSpawns < maxSpawns)
        {
            // Get an enemy object from the object pool.
            GameObject enemyObject = enemyPool.GetObject();
            enemyObject.transform.position = transform.position;
            enemyObject.transform.rotation = transform.rotation;

            // Increase the number of enemies spawned.
            numSpawns++;

            // Wait for the specified delay before spawning the next enemy.
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}