using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public List<Round> rounds; // List of rounds (set in Inspector)
    public Transform[] nodes; // Enemy path nodes
    public Transform enemyContainer;

    private int currentRoundIndex = 0;
    private bool waveIsDone = true;
    private int enemiesAlive = 0;  // Track the number of alive enemies

    public GameManager gameManager;

    void Update()
    {
        // Start the wave spawning when it's done and there are still rounds to go
        if (waveIsDone && currentRoundIndex < rounds.Count)
        {
            StartCoroutine(WaveSpawner());
        }

        // Wait for all enemies to die before moving to the next round
        if (enemiesAlive <= 0 && !waveIsDone)
        {
            waveIsDone = true;
            gameManager.AdvanceRound();
            Debug.Log("advanceRound is called");
            currentRoundIndex++; // Move to the next round
        }
    }

    IEnumerator WaveSpawner()
    {
        waveIsDone = false;

        if (currentRoundIndex >= rounds.Count)
        {
            Debug.Log("All rounds completed!");
            yield break;
        }

        Round currentRound = rounds[currentRoundIndex];

        foreach (RoundEnemy roundEnemy in currentRound.enemies)
        {
            for (int i = 0; i < roundEnemy.count; i++)
            {
                SpawnEnemy(roundEnemy.enemyPrefab);
                yield return new WaitForSeconds(currentRound.spawnRate);
            }
        }

        // After all enemies are spawned, wait until they are all dead
        yield return new WaitUntil(() => enemiesAlive == 0);

        // Once all enemies are dead, proceed to the next round
        waveIsDone = true;
        gameManager.AdvanceRound();
        currentRoundIndex++;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemyClone = Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemyContainer);

        EnemyBehavior enemyBehavior = enemyClone.GetComponent<EnemyBehavior>();
        if (enemyBehavior != null)
        {
            enemyBehavior.SetNodes(nodes);
            enemyBehavior.SetSpwaner(this);
            enemyBehavior.SetGameManger(FindAnyObjectByType<GameManager>());
        }

        enemyClone.GetComponent<EnemyBasklass>()?.SetSpwaner(this);

        // Increase the count of enemies alive when an enemy is spawned
        enemiesAlive++;
    }

    public void EnemyDied()
    {
        // Decrease the count of enemies alive when one dies
        enemiesAlive--;
    }
}
