using UnityEngine;

public class ArmoredEnemy : EnemyBasklass
{
    private bool isArmored;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = startHealth;
        isArmored = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
