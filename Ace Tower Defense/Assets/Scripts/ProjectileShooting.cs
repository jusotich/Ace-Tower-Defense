using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    private Transform target;

    [Header("Variabler")]
    [SerializeField] public float bulletSpeed = 5f;
    [SerializeField] public float projectileDamage = 1f;
    public bool isArmoredPeircing = false;

    public int upgradeLevel = 0;
    public int maxUpgradeLevel = 7;

    public int baseUpgradeCost = 100;

    [Header("Referenser")]
    [SerializeField] private Rigidbody2D rb;

    private void FixedUpdate()
    {
        if(!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * bulletSpeed;
    }

    private void Start()
    {
        Debug.Log("Projectile Layer: " + LayerMask.NameToLayer("Projectile"));
        Debug.Log("Map Layer: " + LayerMask.NameToLayer("Map"));
        Physics2D.IgnoreLayerCollision(7, 8);
    }

    public void SetTarget(Transform _target)
    {
            target = _target;   
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<EnemyBasklass>().TakeDamege(projectileDamage,isArmoredPeircing);
        Debug.Log("Enemy Hit!");
        Destroy(gameObject);
        
    }

    public static class PlayerStats
    {
        public static int Gold = 200;

        public static bool TrySpendGold(int amount)
        {
            if (Gold >= amount)
            {
                Gold -= amount;
                return true;
            }

            return false;
        }

        public static void AddGold(int amount)
        {
            Gold += amount;
        }
    }

    public void UpgradeBulletspeed()
    {
        if (upgradeLevel >= maxUpgradeLevel)
        {
            Debug.Log("Max upgrade level rached!");
            return;
        }

        int cost = GetUpgradeCost();

        if (!PlayerStats.TrySpendGold(cost))
        {
            Debug.Log("Not enough gold to upgrade!");
            return;
        }

        bulletSpeed += 2f;
        Debug.Log($"Tower upgraded to level {upgradeLevel}! Cost: {cost}");
    }

    public void UpgradeDMG()
    {
        if (upgradeLevel >= maxUpgradeLevel)
        {
            Debug.Log("Max upgrade level rached!");
            return;
        }

        int cost = GetUpgradeCost();

        if (!PlayerStats.TrySpendGold(cost))
        {
            Debug.Log("Not enough gold to upgrade!");
            return;
        }

        projectileDamage += 0.5f;
        Debug.Log($"Tower upgraded to level {upgradeLevel}! Cost: {cost}");
    }

    public int GetUpgradeCost()
    {
        return baseUpgradeCost * (upgradeLevel + 1);
    }

    public bool canUpgrade()
    {
        return upgradeLevel < maxUpgradeLevel;
    }
}