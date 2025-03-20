using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public float spawnRate;
    public int enemyThisRound;
    public GameObject[] enemyPrefabs; // Array of enemy prefabs

    public Transform[] nodes; // Path nodes
    public Transform enemyContainer;
    private bool waveIsDone = true;
    private int enemyCount;

    public GameManager gameManager;

    private Dictionary<int, List<int>> enemyWaves = new Dictionary<int, List<int>>();

    void Start()
    {
        SetupEnemyWaves();
    }

    void Update()
    {
        if (waveIsDone)
        {
            StartCoroutine(WaveSpawner());
        }

        if (GetEnemyCount() <= 0)
        {
            waveIsDone = true;
            gameManager.AdvanceRound();
        }
    }

    public void CountEnemies()
    {
        enemyCount = enemyContainer.childCount;
        Debug.Log("Number of enemies: " + enemyCount);
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }

    IEnumerator WaveSpawner()
    {
        waveIsDone = false;

        if (!enemyWaves.ContainsKey(gameManager.round))
        {
            Debug.LogWarning("No wave data found for round " + gameManager.round);
            yield break;
        }

        List<int> enemiesToSpawn = enemyWaves[gameManager.round];

        foreach (int enemyIndex in enemiesToSpawn)
        {
            if (enemyIndex < 0 || enemyIndex >= enemyPrefabs.Length)
            {
                Debug.LogError("Invalid enemy index in round " + gameManager.round);
                continue;
            }

            GameObject enemyClone = Instantiate(enemyPrefabs[enemyIndex], transform.position, Quaternion.identity, enemyContainer);
            CountEnemies();

            EnemyBehavior enemyBehavior = enemyClone.GetComponent<EnemyBehavior>();
            EnemyBasklass enemyBasklass = enemyClone.GetComponent<EnemyBasklass>();

            if (enemyBehavior != null)
            {
                enemyBehavior.SetNodes(nodes);
                enemyBehavior.SetSpwaner(this);
                enemyBasklass.SetSpwaner(this);
                enemyBehavior.SetGameManger(FindAnyObjectByType<GameManager>());
            }
            else
            {
                Debug.LogError("Spawned enemy missing EnemyBehavior script!");
            }

            yield return new WaitForSeconds(spawnRate);
        }

        spawnRate = Mathf.Max(0.1f, spawnRate - 0.1f);
    }

    void SetupEnemyWaves()
    {
        // Defining enemy waves manually
        enemyWaves[1] = new List<int> { 0, 0, 0, 0, 1, 1 }; // Round 1: 4 Red, 2 Blue
        enemyWaves[2] = new List<int> { 0, 1, 1, 2, 2, 2 }; // Round 2: 1 Red, 2 Blue, 3 Green
        enemyWaves[3] = new List<int> { 1, 1, 2, 2, 3 }; // Round 3: 2 Blue, 2 Green, 1 Yellow
        enemyWaves[4] = new List<int> { 2, 3, 3, 4 }; // Round 4: 1 Green, 2 Yellow, 1 Pink
        enemyWaves[5] = new List<int> { 3, 4, 4, 5 }; // Round 5: 1 Yellow, 2 Pink, 1 Armored
        enemyWaves[6] = new List<int> { 4, 4, 5, 5, 5 }; // Round 6: 2 Pink, 3 Armored
    }
}
