using UnityEngine;

public class EnemyBasklass : MonoBehaviour
{
    public GameManager gameManager;
    public float startHealth = 1f;
    protected float health;
    public int damage = 10;
    public int moneyEarned = 10;
    public Spwaner Spwaner;
    public bool isArmored = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = startHealth;
        gameManager = FindAnyObjectByType<GameManager>();
    }
    //get the spwaner refrence for the enemys
    public void SetSpwaner(Spwaner newSpwaner)
    {
        Spwaner = newSpwaner;
    }
    public void TakeDamege(float amount, bool isArmorPiercing) 
    {

        if (isArmored && !isArmorPiercing)
        {
            Debug.Log("enemy is armored! no dmg taken.");
            return;
        }

        health -= amount;

        if (health <= 0)
        {
            gameManager.GetCash(moneyEarned);
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
            Spwaner.Invoke(nameof(Spwaner.EnemyDied), 0.1f); // Small delay to ensure destruction happens first
        }
    }
}
