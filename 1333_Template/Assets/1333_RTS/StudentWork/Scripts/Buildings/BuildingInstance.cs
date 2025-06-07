using UnityEngine;

// Represents a real, in-game building on the map.
public class BuildingInstance : BuildingBase
{
    // Reference to this instance's configuration.
    public BuildingType Config => buildingType;

    // Set up this building with a specific configuration.
    public void Initialize(BuildingType config)
    {
        buildingType = config;
    }

    // Places the building on a specific node in the grid.
    public override void PlaceOnNode(GridNode node)
    {
        Debug.Log($"Building placed at node: {node.Name}, world position: {node.WorldPosition}");
        // Add placement logic here.
    }
}
