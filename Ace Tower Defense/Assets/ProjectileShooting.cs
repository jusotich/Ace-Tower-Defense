using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    bool shoot;//Placeholder

    public Transform firePoint;

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



    }
}
