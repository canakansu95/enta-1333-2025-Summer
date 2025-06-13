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
}
