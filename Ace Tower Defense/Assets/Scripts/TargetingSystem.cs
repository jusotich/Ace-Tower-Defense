using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class TargetingSystem : MonoBehaviour
{

    [SerializeField] private float range;

    private void Awake()
    {
        
    }

    private void Update()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider2D in colliderArray)
        {
            if(collider2D.TryGetComponent<*ReplaceWithSecipt*>(out *ReplaceWithScript* enemy))
            {

            }
        }
    }





}
