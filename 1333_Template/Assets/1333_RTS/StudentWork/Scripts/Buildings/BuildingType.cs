
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingType", menuName = "Game/BuildingType")]
public class BuildingType : ScriptableObject
{
    public string BuildingName;
    public Sprite BuildingIcon;
    [SerializeField] private int width = 1;
    [SerializeField] private int height = 1;

    public int Width => width;
    public int Height => height;
}
