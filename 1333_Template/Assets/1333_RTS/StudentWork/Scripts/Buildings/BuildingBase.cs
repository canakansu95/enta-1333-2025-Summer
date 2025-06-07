using UnityEngine;

// Abstract base for all buildings in the game grid.
public abstract class BuildingBase : MonoBehaviour
{
    // Data about the type of building, like size or abilities.
    [SerializeField] protected BuildingType buildingType;

    // Width (in grid cells) this building occupies.
    public virtual int Width => buildingType != null ? buildingType.Width : 1;

    // Height (in grid cells) this building occupies.
    public virtual int Height => buildingType != null ? buildingType.Height : 1;

    // Must be implemented by subclasses to place this building at a grid node.
    public abstract void PlaceOnNode(GridNode node);
}