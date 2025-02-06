using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour 
{
    public static BuildManager main;


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
}
