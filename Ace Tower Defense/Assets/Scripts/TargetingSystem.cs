using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class TargetingSystem : MonoBehaviour
{
    [Header("Variabler")]
    [SerializeField] private float range = 5f;
    [SerializeField] private float bps = 1f; //bullets per second

    [Header("Referenser")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform fireingPoint;

    private float timeUntillFire;
    private Transform target = null;

    private void OnDrawGizmosSelected()
    {
       Handles.DrawWireDisc(transform.position, transform.forward, range);
       Handles.color = Color.magenta;
    }


    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        if (!CheckTargetInRange())
        {
            target = null;
        }
        else
        {
            timeUntillFire += Time.deltaTime;
            if(timeUntillFire >= 1f/bps)
            {
                Shoot();
                timeUntillFire = 0f;
            }
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
        
    }

    private bool CheckTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= range; 
    }

    private void Shoot()
    {
        GameObject Projectile = Instantiate(projectilePrefab, fireingPoint.position, Quaternion.identity);
        ProjectileShooting projectileScript = Projectile.GetComponent<ProjectileShooting>();
        projectileScript.SetTarget(target);
    }

}
