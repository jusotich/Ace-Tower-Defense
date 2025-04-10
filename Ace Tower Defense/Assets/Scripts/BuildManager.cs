using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    public Transform towerContainer;

    [Header("Referenser")]
    [SerializeField] public GameObject[] towerPrefabs;
    [SerializeField] private Collider2D mapCollider; // Reference to the map collider

    private bool towerUpgradeOpen = false;
    private TargetingSystem activeTowerUI = null;
    private int selectedCardIndex = 0;
    private bool canPlace = false;
    private GameObject tower;

    private void OnMouseDown()
    {
        Debug.Log("Buildmanager: Clicked collider");
        Debug.Log("Buildmanager: Can place tower: " + canPlace);
        if (!canPlace) return;

        GameObject towerToBuild = GetSelectedTower();

        Vector2 mouseWorldPos = GetMouseWorldPosition();
        tower = Instantiate(towerToBuild, mouseWorldPos, Quaternion.identity, towerContainer);

        Debug.Log("Buildmanager: " + selectedCardIndex);
        // Efter placering – stäng av byggläget
        canPlace = false;
        selectedCardIndex = 0;
    }



    private void Awake()
    {
        main = this;
    }
    public void SelectTower(int cardIndex, bool allowPlacement)
    {
        selectedCardIndex = cardIndex;
        canPlace = allowPlacement;
        Debug.Log("Selected tower index: " + selectedCardIndex);
    }


    public GameObject GetSelectedTower()
    {
        if (selectedCardIndex >= 0 && selectedCardIndex < towerPrefabs.Length)
            return towerPrefabs[selectedCardIndex];

        Debug.LogWarning("Invalid card index");
        return towerPrefabs[0]; // fallback
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

        if (hit.collider != null)         // Klickade p� n�got
        {

            TargetingSystem tower = hit.collider.GetComponent<TargetingSystem>();

            if (tower != null) // Klickade p� ett torn
            {
                // Om vi redan har detta torns UI �ppet, g�r ingenting
                if (activeTowerUI == tower && towerUpgradeOpen)
                {
                    return;
                }

                // Om en annan UI �r �ppen, st�ng den f�rst
                if (towerUpgradeOpen)
                {
                    TargetingSystem.CloseAllUpgradeUIs();
                    towerUpgradeOpen = false;
                    activeTowerUI = null;
                }

                // �ppna det nya tornets UI
                tower.OpenUpgradeUI();
                towerUpgradeOpen = true;
                activeTowerUI = tower;
                return;
            }
        }

        // If upgrade UI is open and we click anywhere else, close all UIs
        if (towerUpgradeOpen)
        {
            Debug.Log("Closing all upgrade UIs");
            TargetingSystem.CloseAllUpgradeUIs();
            towerUpgradeOpen = false;
            activeTowerUI = null; // Gl�m vilket torn som var aktivt
            return;
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