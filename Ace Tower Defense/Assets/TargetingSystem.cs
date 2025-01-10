using UnityEngine;
using UnityEngine.AI;

public class TargetingSystem : MonoBehaviour
{

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Transform.LookAt(enemy);
        }


    }

    public void SetTarget(Enemy targetEnemy)
    {
        this.tergetEnemy = targetEnemy;
    }

}
