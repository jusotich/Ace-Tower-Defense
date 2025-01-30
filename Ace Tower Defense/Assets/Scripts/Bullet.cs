using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    bool shoot;//Placeholder

    public Transform firePoint;
    public GameObject bulletPrefab;
    [SerializeField] private int projectileDamage = 1;

    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //shooting Logic

        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<EnemyBasklass>().TakeDamege(projectileDamage);
        Debug.Log("Enemy hit!");
        Destroy(gameObject);

    }
}
