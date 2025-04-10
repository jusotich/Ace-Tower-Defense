using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [SerializeField] public GameObject[] towerPrefabs;
    public Transform towerContainer;

    private GameObject tower;
    private int selectedCardIndex = 0;
    private bool canPlace = false;


    private void OnMouseDown()
    {
        Debug.Log("Buildmanager: Clicked collider");
        Debug.Log("Buildmanager: Can place tower: " + canPlace);
        if (!canPlace) return;

        GameObject towerToBuild = GetSelectedTower();

        Vector2 mouseWorldPos = GetMouseWorldPosition();
        tower = Instantiate(towerToBuild, mouseWorldPos, Quaternion.identity, towerContainer);

        Debug.Log("Buildmanager: " + selectedCardIndex);
        // Efter placering � st�ng av byggl�get
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

    //dodo code - william
    //Jag vet - Joachim


    private Vector2 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}