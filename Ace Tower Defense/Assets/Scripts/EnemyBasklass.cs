using UnityEngine;

public class EnemyBasklass : MonoBehaviour
{
    public float startHealth = 1f;
    private float health;
    public int damage = 10;
    public float speed;
    public int moneyEarned = 10;
    public Spwaner Spwaner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = startHealth;
    }
    //get the spwaner refrence for the enemys
    public void SetSpwaner(Spwaner newSpwaner)
    {
        Spwaner = newSpwaner;
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
        Debug.Log("Killing the target");
        Destroy(gameObject);

        // Delay enemy count update to ensure it's removed from the container
        if (Spwaner != null)
        {
            Spwaner.Invoke(nameof(Spwaner.CountEnemies), 0.1f); // Small delay to ensure destruction happens first
        }
    }
}
