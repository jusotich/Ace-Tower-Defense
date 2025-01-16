using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class TargetingSystem : MonoBehaviour
{
    [Header("Variabler")]
    [SerializeField] private float range = 5f;

    [Header("Referenser")]
    [SerializeField] private Transform TurretRotationPoint;

    private Transform target = null;

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.magenta;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
    }

    private void FindTarget()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider2D in colliderArray)
        {
            /*if(collider2D.TryGetComponent<ReplaceWithSecipt>(out ReplaceWithScript enemy))
              {

              }*/
        }
    }





}
