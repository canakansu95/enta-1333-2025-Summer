using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

// Holds settings for a building type (e.g. barracks, factory).
[CreateAssetMenu(fileName = "BuildingType", menuName = "Game/BuildingType")]
public class BuildingType : ScriptableObject
{
   
   public List<BuildingData> Buildings = new ();
    

    [SerializeField] private int width = 1;
    [SerializeField] private int height = 1;

    // How many grid cells wide this building is.
    public int Width => width;
    // How many grid cells tall this building is.
    public int Height => height;
}


[System.Serializable]
public class BuildingData
{
    public string BuildingName;
    public Sprite BuildingIcon;
}
