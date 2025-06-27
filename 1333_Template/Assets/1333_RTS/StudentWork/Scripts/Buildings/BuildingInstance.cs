using UnityEngine;

public class BuildingInstance : BuildingBase  // in-game building instances
{
    public BuildingType Config => buildingType;

    [SerializeField] private Material[] teamMaterials; 

    public int TeamId { get; private set; } = 0; // set on placement

    
    public void Initialize(BuildingType config, int teamId = 0) // set up this building with config and team id
    {
        buildingType = config;
        TeamId = teamId;
    }

    
    public override void PlaceOnNode(GridNode node) // places the building on a specific node in the grid.
    {
        // transform.position = node.WorldPosition;
       
    }

   
    public void SpawnUnit(GameObject unitPrefab, AStarPathfinder pathfinder)   // spawns a unit prefab at a valid free adjacent cell.
    {
        GridManager gridManager = FindObjectOfType<GridManager>(); // reference to grid manager in the scene
        Vector2Int buildingGridPos = gridManager.WorldToGridIndex(transform.position);

        for (int dx = -1; dx <= Width; dx++)      // check edges for adjacent spawn point
        {
            for (int dy = -1; dy <= Height; dy++)
            {
                if (dx == -1 || dx == Width || dy == -1 || dy == Height)
                {
                    int x = buildingGridPos.x + dx;
                    int y = buildingGridPos.y + dy;
                    if (gridManager.IsInBounds(x, y))
                    {
                        var node = gridManager.GetNode(x, y);
                        if (node.Walkable && !node.Occupied)
                        {
                            Vector3 spawnPos = node.WorldPosition;
                            GameObject go = Instantiate(unitPrefab, spawnPos, Quaternion.identity);

                            var unitInstance = go.GetComponent<UnitInstance>();
                            if (unitInstance != null)
                            {
                              
                                Material teamMat = null;
                                if (teamMaterials != null && TeamId < teamMaterials.Length)
                                    teamMat = teamMaterials[TeamId];

                                unitInstance.Initialize(pathfinder, teamMat); // set pathfinder and material
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
