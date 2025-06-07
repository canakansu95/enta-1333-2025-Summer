using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour     // the main controller that ties together grid, units, pathfinding, and armies.
{
    [Header("Main System References")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private AStarPathfinder pathfinder;
    [SerializeField] private Transform startMarker;
    [SerializeField] private Transform endMarker;
    [SerializeField] private LineRenderer pathLine;
    [SerializeField] private ArmyPathfindingTester pathfindingTester;

    [Header("Marker & Army Settings")]
    [SerializeField] private float markerHeight = 0.5f;
    [SerializeField] private AvailableUnits defaultUnits;


    [Header("Terrain Settings")]
    [SerializeField] private List<TerrainType> terrains;

    


    private TeamArmies allTeams = new TeamArmies();

 
    private void Awake()        // initialize grid and spawn initial units/armies.
    {
        if (!AreReferencesSet())
        {
            Debug.LogError("Missing");
            enabled = false;
            return;
        }

        gridManager.InitializeGrid();
        unitManager.SpawnDummyUnit(startMarker);
        unitManager.SpawnDummyUnit(endMarker);
        pathfindingTester.Initialize();  // spawn 2 armies on awake one for player one for npc
        RandomizePathAndMarkers();

       
    }

   



    private bool AreReferencesSet()    // bool verifies all required fields are set.
    {
        return gridManager && unitManager && pathfinder && startMarker && endMarker && pathLine;
    }


    private void RandomizePathAndMarkers() // function to randomize markers and find a new path.
    {
        if (!gridManager.IsInitialized)
            gridManager.InitializeGrid();

        for (int x = 0; x < gridManager.GridSettings.GridSizeX; x++)
        {
            for (int y = 0; y < gridManager.GridSettings.GridSizeY; y++)
            {
                TerrainType type = terrains[Random.Range(0, terrains.Count)];
                GridNode node = gridManager.GetNode(x, y);
                node.Walkable = type.IsWalkable;
                node.Weight = type.MovementCost;
                node.TerrainType = type;
                gridManager.SetNode(x, y, node);
            }
        }
    }

    private void Update() // space key randomizing the grid and terraintpyes
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RandomizePathAndMarkers();
            
        }
    }
}


