using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public List<Round> rounds; // List of rounds (set in Inspector)
    public Transform[] nodes; // Enemy path nodes
    public Transform enemyContainer;

    private int currentRoundIndex = 0;
    private bool waveIsDone = true;
    public int enemiesAlive = 0;

    public GameManager gameManager;

    void Update()
    {
        if (waveIsDone && currentRoundIndex < rounds.Count)
        {
            StartCoroutine(WaveSpawner());
        }

        if (enemiesAlive <= 0)
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
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemyClone = Instantiate(enemyPrefab, transform.position, Quaternion.identity, enemyContainer);
        enemiesAlive++;

        EnemyBehavior enemyBehavior = enemyClone.GetComponent<EnemyBehavior>();
        if (enemyBehavior != null)
        {
            enemyBehavior.SetNodes(nodes);
            enemyBehavior.SetSpwaner(this);
            enemyBehavior.SetGameManger(FindAnyObjectByType<GameManager>());
        }

        enemyClone.GetComponent<EnemyBasklass>()?.SetSpwaner(this);
    }

    public void EnemyDied()
    {
        enemiesAlive--;
    }
}