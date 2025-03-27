using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    public Transform towerContainer;

    [Header("Referenser")]
    [SerializeField] private GameObject[] towerPrefabs;

    private int SelectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    public GameObject GetSelectedTower()
    {
        return towerPrefabs[SelectedTower];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left-click
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        Vector2 mouseWorldPos = GetMouseWorldPosition();
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null) // Clicked on something
        {
            TargetingSystem tower = hit.collider.GetComponent<TargetingSystem>();
            if (tower != null)
            {
                Debug.Log("Tower clicked! Opening upgrade UI...");
                tower.OpenUpgradeUI();
                return;
            }
        }

        // If clicked on empty space, place a new tower
        GameObject towerToBuild = GetSelectedTower();
        GameObject newTower = Instantiate(towerToBuild, mouseWorldPos, Quaternion.identity, towerContainer);
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
