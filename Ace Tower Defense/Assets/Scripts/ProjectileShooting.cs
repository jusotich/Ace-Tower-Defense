using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    [Header("Variabler")]
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int projectileDamage = 1;

    [Header("Referenser")]
    [SerializeField] private Rigidbody2D rb;

    private void FixedUpdate()
    {
        if(!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * bulletSpeed;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<EnemyBasklass>().TakeDamege(projectileDamage);
        Debug.Log("Enemy Hit!");
        Destroy(gameObject);
        
    }

}