using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuildingButton : MonoBehaviour
{

    [SerializeField] private Image buttonImage;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Button button;

    private BuildingData buildingDataForButton;


    public void Setup(BuildingData buildingData)
    {
        buildingDataForButton = buildingData;
        buttonText.text = buildingDataForButton.BuildingName;

        //optional
        buttonImage.sprite = buildingDataForButton.BuildingIcon;
    }
}
