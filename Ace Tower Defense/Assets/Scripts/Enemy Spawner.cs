using System.Collections;
using UnityEngine;

public class Spwaner : MonoBehaviour
{
    public float spawnRate = 1.0f;
    public float timeBetweenWaves = 3.0f;

    public int enemyCount;

    public GameObject enemy;

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

        for (int i = 0; i < enemyCount; i++) //Ser till att det spawnar en wave av fiender
        {
            GameObject enemyClone = Instantiate(enemy);

            yield return new WaitForSeconds(spawnRate); //En delay mellan spawnandet
        }

        //För att göra waves svårare och svårare
        spawnRate -= 0.1f;
        enemyCount += 3;

        yield return new WaitForSeconds(timeBetweenWaves); //Delay mellan waves

        waveIsDone = true;
    }
}
