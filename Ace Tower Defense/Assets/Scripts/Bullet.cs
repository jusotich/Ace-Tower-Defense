using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    bool shoot;//Placeholder

    public Transform firePoint;
    public GameObject bulletPrefab;

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
}
