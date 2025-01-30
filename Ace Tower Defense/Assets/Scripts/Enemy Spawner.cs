using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class Spwaner : MonoBehaviour
{
    public float spawnRate;
    public int enemyCount;
    public GameObject enemy; // Prefab of the enemy
    public Transform[] nodes; // Path nodes to pass to enemies
    bool waveIsDone = true;
    private int currentEnemyOnScreen = 0;

    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if (waveIsDone == true)
        {
            StartCoroutine(waveSpawner());
        }

        if (currentEnemyOnScreen <= 0)
        {
            waveIsDone = true;
            gameManager.AdvanceRound();
        }
    }
    public void EnemyDied()
    {
        gameManager.TakeDamage();
        currentEnemyOnScreen--;
    }
    IEnumerator waveSpawner()
    {
        waveIsDone = false;

        for (int i = 0; i < enemyCount; i++) // Spawns a wave of enemies
        {
            currentEnemyOnScreen++;
            // Instantiate the enemy
            GameObject enemyClone = Instantiate(enemy, transform.position, Quaternion.identity);

            // Pass the nodes and spwaner to the enemy's EnemyBehavior script
            EnemyBehavior enemyBehavior = enemyClone.GetComponent<EnemyBehavior>();
            if (enemyBehavior != null)
            {
                enemyBehavior.SetNodes(nodes); // Assign the path nodes
                enemyBehavior.SetSpwaner(this);
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
    }
}