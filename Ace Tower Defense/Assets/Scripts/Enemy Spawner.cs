using System.Collections;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public float spawnRate = 1.0f;
    public float timeBetweenWaves = 3.0f;

    public int enemyCount;

    public GameObject enemy; // Prefab of the enemy

    public Transform[] nodes; // Path nodes to pass to enemies

    bool waveIsDone = true;

    // Update is called once per frame
    void Update()
    {
        if (waveIsDone == true)
        {
            StartCoroutine(waveSpawner());
        }
    }

    IEnumerator waveSpawner()
    {
        waveIsDone = false;

        for (int i = 0; i < enemyCount; i++) // Spawns a wave of enemies
        {
            // Instantiate the enemy
            GameObject enemyClone = Instantiate(enemy, transform.position, Quaternion.identity);

            // Pass the nodes to the enemy's EnemyBehavior script
            EnemyBehavior enemyBehavior = enemyClone.GetComponent<EnemyBehavior>();
            if (enemyBehavior != null)
            {
                enemyBehavior.SetNodes(nodes); // Assign the path nodes
            }
            else
            {
                Debug.LogError("The spawned enemy is missing the EnemyBehavior script!");
            }

            yield return new WaitForSeconds(spawnRate); // Delay between spawns
        }

        // Make waves progressively harder
        spawnRate = Mathf.Max(0.1f, spawnRate - 0.1f); // Clamp spawn rate to avoid going negative
        enemyCount += 3;

        yield return new WaitForSeconds(timeBetweenWaves); // Delay between waves

        waveIsDone = true;
    }
}
