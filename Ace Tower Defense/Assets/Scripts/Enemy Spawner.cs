using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Spwaner : MonoBehaviour
{
    public float spawnRate;
    public int enemyThisRound;
    public GameObject enemy; // Prefab of the enemy
    public Transform[] nodes; // Path nodes to pass to enemies
    bool waveIsDone = true;
    public Transform enemyContaier;

    private int enemyCount; // Store the enemy count

    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if (waveIsDone == true)
        {
            StartCoroutine(WaveSpawner());
        }

        if (GetEnemyCount() <= 0 )
        {
            waveIsDone = true;
            gameManager.AdvanceRound();
        }
    }
    public void CountEnemies()
    {
        enemyCount = enemyContaier.childCount;
        Debug.Log("number of enemies: " + enemyCount);

    }
    public int GetEnemyCount()
    {
        return enemyCount;
    }

    IEnumerator WaveSpawner()
    {

        waveIsDone = false;

        for (int i = 0; i < enemyThisRound; i++) // Spawns a wave of enemies
        {
            // Instantiate the enemy
            GameObject enemyClone = Instantiate(enemy, transform.position, Quaternion.identity, enemyContaier);
            CountEnemies();

            // Pass the nodes and spwaner to the enemy's EnemyBehavior script
            EnemyBehavior enemyBehavior = enemyClone.GetComponent<EnemyBehavior>();
            EnemyBasklass enemyBasklass = enemyClone.GetComponent<EnemyBasklass>();
            if (enemyBehavior != null)
            {
                enemyBehavior.SetNodes(nodes); // Assign the path nodes
                enemyBehavior.SetSpwaner(this);
                enemyBasklass.SetSpwaner(this);
            }
            else
            {
                Debug.LogError("The spawned enemy is missing the EnemyBehavior script!");
            }

            yield return new WaitForSeconds(spawnRate); // Delay between spawns
        }


        // Make waves progressively harder
        spawnRate = Mathf.Max(0.1f, spawnRate - 0.1f); // Clamp spawn rate to avoid going negative
        enemyThisRound += 3;
    }
}