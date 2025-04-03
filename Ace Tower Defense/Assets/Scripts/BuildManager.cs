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
    [SerializeField] private Collider2D mapCollider; // Reference to the map collider

    private int SelectedTower = 0;
    private bool towerUpgradeOpen = false;
    private TargetingSystem currentTower = null;

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

            if (tower != null) // Clicked on a tower
            {
                //if clicking tha same towet = ingore
                if(tower == currentTower&& towerUpgradeOpen)
                {
                    return;
                }


                if (towerUpgradeOpen)
                {
                    TargetingSystem.CloseAllUpgradeUIs();// Call the static method

                }
                Debug.Log("Tower clicked! Opening upgrade UI...");
                tower.OpenUpgradeUI();
                towerUpgradeOpen = true;
                currentTower = tower;
                return; // Stop further execution (don't build a tower)
            }
        }

        // If upgrade UI is open and we click anywhere else, close all UIs
        if (towerUpgradeOpen)
        {
            Debug.Log("Closing all upgrade UIs");
            TargetingSystem.CloseAllUpgradeUIs();
            towerUpgradeOpen = false;
            currentTower = null;
            return; // Stop further execution (don't build a tower)
        }

        // Check if the clicked position is inside the map collider
        if (mapCollider != null && mapCollider.OverlapPoint(mouseWorldPos))
        {
            // If no UI is open and we clicked on the map, place a new tower
            GameObject towerToBuild = GetSelectedTower();
            Instantiate(towerToBuild, mouseWorldPos, Quaternion.identity, towerContainer);
        }
        else
        {
            Debug.Log("Invalid placement! Clicked outside the map area.");
        }
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}