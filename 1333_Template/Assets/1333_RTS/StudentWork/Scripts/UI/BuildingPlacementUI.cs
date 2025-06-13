
using UnityEngine;
using UnityEngine.UI;

public class BuildingPlacementUI : MonoBehaviour   // displays ui buttons for each building and tells controller what to place.
{
    [SerializeField] private RectTransform layoutGroupParent;
    [SerializeField] private SelectBuildingButton buttonPrefab;
    [SerializeField] private BuildingTypePrefab[] buildingTypePrefabs;
    [SerializeField] private BuildingPlacementController placementController;

    void Start()
    {
        foreach (var typePrefab in buildingTypePrefabs)
        {
            var button = Instantiate(buttonPrefab, layoutGroupParent);
            button.Setup(typePrefab.buildingType.BuildingName, typePrefab.buildingType.BuildingIcon);
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                placementController.SetBuildingToPlace(typePrefab);
            });
        }
    }
}
