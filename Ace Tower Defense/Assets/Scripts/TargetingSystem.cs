using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class TargetingSystem : MonoBehaviour
{
    [Header("Variabler")]
    [SerializeField] public float range = 5f;
    [SerializeField] public float bps = 1f; //bullets per second

    public int upgradeLevel = 0;
    public int maxUpgradeLevel = 10;

    public int baseUpgradeCost = 100;

    [Header("Referenser")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform fireingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;

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

    public void OpenUpgradeUI()
    {
        if (!upgradeUI.activeSelf)
        {
            upgradeUI.SetActive(true);
            Debug.Log("upgradeUI works");
        }
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        Debug.Log("UpgradeUI closing works");
    }
    // NEW: Static method to close ALL upgrade UIs
    public static void CloseAllUpgradeUIs()
    {
        TargetingSystem[] allTowers = FindObjectsByType<TargetingSystem>(FindObjectsSortMode.None);

        foreach (TargetingSystem tower in allTowers)
        {
            tower.CloseUpgradeUI();
        }
    }

    public void UpgradeRange()
    {
        if (upgradeLevel >= maxUpgradeLevel)
        {
            Debug.Log("Max upgrade level rached!");
            return;
        }

        int cost = GetUpgradeCost();

        if (!PlayerStats.TrySpendGold(cost))
        {
            Debug.Log("Not enough gold to upgrade!");
            return;
        }

        range += 0.3f;
        cost += 500;

        Debug.Log($"Tower upgraded to level {upgradeLevel}! Cost: {cost}");
    }

    public static class PlayerStats
    {
        public static int Gold = 200;

        public static bool TrySpendGold(int amount)
        {
            if (Gold >= amount)
            {
                Gold -= amount;
                return true;
            }

            return false;
        }

        public static void AddGold(int amount)
        {
            Gold += amount;
        }
    }

    public void UpgradeBPS()
    {
        if (upgradeLevel >= maxUpgradeLevel)
        {
            Debug.Log("Max upgrade level rached!");
            return;
        }

        int cost = GetUpgradeCost();

        if (!PlayerStats.TrySpendGold(cost))
        {
            Debug.Log("Not enough gold to upgrade!");
            return;
        }

        bps += 1f;
        Debug.Log($"Tower upgraded to level {upgradeLevel}! Cost: {cost}");
    }

    public int GetUpgradeCost()
    {
        return baseUpgradeCost * (upgradeLevel + 1);
    }

    public bool canUpgrade()
    {
        return upgradeLevel < maxUpgradeLevel;
    }
}
