using UnityEngine;


public class BuildingInstance : BuildingBase  // in game building instances
{
  
    public BuildingType Config => buildingType;


    public void Initialize(BuildingType config)
    {
        buildingType = config;
    }

 
    public override void PlaceOnNode(GridNode node)
    {

      
       // transform.position = node.WorldPosition;   

        Debug.Log($"Building placed at node: {node.Name}, world position: {node.WorldPosition}");
        
    }


    public void SpawnUnit(GameObject unitPrefab)
    {
        
        GridManager gridManager = FindObjectOfType<GridManager>();     // reference to grid manager in the scene.
        Vector2Int buildingGridPos = gridManager.WorldToGridIndex(transform.position);

     
        for (int dx = -1; dx <= Width; dx++)      // spawn on a free adjacent cell (edge of the building)
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
                            Instantiate(unitPrefab, spawnPos, Quaternion.identity);
                     
                            return;
                        }
                    }
                }
            }
        }
       
    }

}
