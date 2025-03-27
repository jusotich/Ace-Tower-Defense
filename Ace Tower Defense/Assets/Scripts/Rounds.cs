using System;
using UnityEngine;

[Serializable]
public class Round
{
    public RoundEnemy[] enemies;  // List of enemies in this round
    public float spawnRate = 1f;  // Time between spawns
}

[Serializable]
public class RoundEnemy
{
    public GameObject enemyPrefab; // The enemy type
    public int count;              // How many to spawn
}
