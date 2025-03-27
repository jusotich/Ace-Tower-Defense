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

    public GameObject towerObj;
    public TargetingSystem tower;

    private int SelectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    public GameObject GetSelectedTower()
    {
        return towerPrefabs[SelectedTower];
    }
    
    //dodo code
    private void OnMouseDown()
    {
        if (towerObj != null)
        {
            tower.OpenUpgradeUI();
            
        }

        GameObject towerToBuild = GetSelectedTower();
        Vector2 mouseWorldPos = GetMouseWorldPosition();


        towerObj = Instantiate(towerToBuild, mouseWorldPos, Quaternion.identity, towerContainer); towerObj = null;
        tower = towerObj.GetComponent<TargetingSystem>();
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
