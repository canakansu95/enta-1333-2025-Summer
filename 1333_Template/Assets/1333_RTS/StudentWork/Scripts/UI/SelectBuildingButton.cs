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

   


    public void Setup(string buildingName, Sprite buildingIcon)
    {
        buttonText.text = buildingName;
        buttonImage.sprite = buildingIcon;
    }
}
