using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BuildManager : MonoBehaviour 
{
    public static BuildManager main;


    [Header("Referenser")]
    [SerializeField] private GameObject[] towerPrefabs;

    private GameObject tower;

    private int SelectedTower = 0;

    private void Awake()
    {
        main = this;
    }

    public GameObject GetSelectedTower()
    {
        return towerPrefabs[SelectedTower];
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        GameObject towerToBuild = GetSelectedTower();
        Vector2 mouseWorldPos = GetMouseWorldPosition();


        tower = Instantiate(towerToBuild, mouseWorldPos, Quaternion.identity); tower = null;
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
