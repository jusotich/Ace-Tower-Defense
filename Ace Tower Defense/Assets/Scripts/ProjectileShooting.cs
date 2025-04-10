using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    private Transform target;

    [Header("Variabler")]
    [SerializeField] public float bulletSpeed = 5f;
    [SerializeField] public float projectileDamage = 1f;
    private bool isArmoredPeircing = false;

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

    public void UpgradeBulletSpeed()
    {
        bulletSpeed += 2f;

        Debug.Log($"Tower upgraded! New stats -> Bulletspeed {bulletSpeed}");
    }

    public void UpgradeDMG()
    {
        projectileDamage += 0.5f;

        Debug.Log($"Tower upgraded! New stats -> Damage {projectileDamage}");
    }
}