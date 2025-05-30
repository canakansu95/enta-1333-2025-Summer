using System.Collections.Generic;
using UnityEngine;

public class AStarTester : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GridManager gridManager;
    [SerializeField] private AStarPathfinder aStarPathfinder;
    [SerializeField] private Transform startMarker;
    [SerializeField] private Transform endMarker;
    [SerializeField] private LineRenderer pathLineRenderer;

    [Header("Terrain Randomization")]
    [SerializeField] private List<TerrainType> availableTerrains;

    [Header("Path Debug")]
    [SerializeField] private Color pathColor = Color.yellow;
    [SerializeField] private Color visitedColor = Color.blue;
    [SerializeField] private Color frontierColor = Color.cyan;
    private List<GridNode> lastPath;

    void Start()
    {
        RandomizeTerrain();
        RunAStar();
    }

    public void RandomizeTerrain() // randomizing function
    {
        if (!gridManager.IsInitialized)
            gridManager.InitializeGrid();

        for (int x = 0; x < gridManager.GridSettings.GridSizeX; x++)
        {
            for (int y = 0; y < gridManager.GridSettings.GridSizeY; y++)
            {
                TerrainType randTerrain = availableTerrains[Random.Range(0, availableTerrains.Count)];
                GridNode node = gridManager.GetNode(x, y);
                node.Walkable = randTerrain.IsWalkable;
                node.Weight = randTerrain.MovementCost;
                gridManager.SetNode(x, y, node);
            }
        }
    }

    public void RunAStar() // same as bruteforce find path
    {
        lastPath = aStarPathfinder.FindPath(startMarker.position, endMarker.position);
        DrawPathWithLineRenderer();
    }

    private void DrawPathWithLineRenderer()
    {
        if (pathLineRenderer == null) return;
        if (lastPath == null || lastPath.Count == 0)
        {
            pathLineRenderer.positionCount = 0;
            return;
        }

        pathLineRenderer.positionCount = lastPath.Count;
        for (int i = 0; i < lastPath.Count; i++)
        {
            pathLineRenderer.SetPosition(i, lastPath[i].WorldPosition + Vector3.up * 0.05f);
        }
        pathLineRenderer.startColor = pathColor;
        pathLineRenderer.endColor = pathColor;
    }

    private void OnDrawGizmos()
    {
        if (gridManager == null || aStarPathfinder == null) return;    // draw visited nodes

        Gizmos.color = visitedColor;
        foreach (var pair in aStarPathfinder.VisitedNodes)
        {
            if (!pair.Value) continue;
            var node = gridManager.GetNode(pair.Key.x, pair.Key.y);
            Gizmos.DrawCube(node.WorldPosition, Vector3.one * gridManager.GridSettings.NodeSize * 0.4f);
        }
        
        Gizmos.color = frontierColor;
        foreach (var pair in aStarPathfinder.FrontierNodes) //// draw frontier nodes
        {
            if (!pair.Value) continue;
            var node = gridManager.GetNode(pair.Key.x, pair.Key.y);
            Gizmos.DrawCube(node.WorldPosition, Vector3.one * gridManager.GridSettings.NodeSize * 0.6f);
        }
       
        if (lastPath != null && lastPath.Count > 1) //  // draw final path
        {
            Gizmos.color = pathColor;
            for (int i = 0; i < lastPath.Count - 1; i++)
            {
                Gizmos.DrawLine(lastPath[i].WorldPosition, lastPath[i + 1].WorldPosition);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // pressing space to randomize terrain and rerun pathfinder
        {
            RandomizeTerrain();
            RunAStar();
        }
    }
}
