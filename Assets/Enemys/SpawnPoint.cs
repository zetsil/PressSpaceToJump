using UnityEngine;
using System.Collections;


public class SpawnPoint : MonoBehaviour
{
  
    public GameObject enemyPrefab;  // Assign the enemy prefab here
    public float spawnInterval = 5f; // Time in seconds between each spawn
    private bool isSpawning = true;

    private void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        // Check if enemyPrefab is assigned before proceeding
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy prefab is not assigned. Aborting spawn routine.");
            yield break; // Exit the coroutine if enemyPrefab is not set
        }

        // Keep spawning while enabled
        while (isSpawning )
        {
            yield return new WaitForSeconds(spawnInterval);

            // Check if enemyPrefab is assigned before spawning
            if (enemyPrefab != null)
            {
                // Spawn the enemy at this GameObject's position and rotation
                GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
                isSpawning = false;

                // Find any ParticleSystem component on the spawned enemy or its children and destroy it
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("Enemy prefab is not assigned!");
                isSpawning = false; // Stop spawning if no prefab is assigned
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Change color as needed
        Gizmos.DrawSphere(transform.position, 0.4f); // Draw a small sphere
    }

    public void StopSpawning()
    {
        // Stop spawning enemies
        isSpawning = false;
    }
}
