using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    private Transform target;

    [Header("Variabler")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int projectileDamage = 1;
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

}