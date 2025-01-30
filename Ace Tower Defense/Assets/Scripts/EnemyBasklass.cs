using UnityEngine;

public class EnemyBasklass : MonoBehaviour
{
    public float startHealth = 1f;
    private float health;
    public int damage = 10;
    public float speed;
    public int moneyEarned = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void TakeDamege(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // är menad för att ge pengar till spelaren
        // PlayerStats.Money += moneyEarned;

        // vet inte om är nödvändig men skrev ändå
        // WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }
}
