using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GridSettings gridSettings; // referances
    public GridSettings GridSettings => gridSettings;

    private GridNode[,] gridNodes;

#if UNITY_EDITOR

    [Header("Debug for editor playmode only")]
    [SerializeField] private List<GridNode> AllNodes = new();
    [SerializeField] private bool showGrid = true;
#endif


    public bool IsInitialized { get; private set; } = false;

    public void InitializeGrid()  // creates the grid with along with gridSettings
    {
        gridNodes = new GridNode[gridSettings.GridSizeX, gridSettings.GridSizeY]; // nested loop

        for (int x = 0; x < gridSettings.GridSizeX; x++)
        {

            for (int y = 0; y < gridSettings.GridSizeY; y++)

            {

                Vector3 worldPos = gridSettings.UseXZPlane
                ? new Vector3(x, 0, y) * gridSettings.NodeSize
                : new Vector3(x, y, 0) * gridSettings.NodeSize;


                GridNode node = new GridNode
                {

                    Name = $"Cell_{x}_{y}",
                    WorldPosition = worldPos,
                    Walkable = true, 
                    Weight = 1
                };

                gridNodes[x, y] = node; 
            }

        }
        IsInitialized = true;
     
    }


   

    private void OnDrawGizmos()
    {
        if (!showGrid || gridNodes == null || gridSettings == null) return;


        

        for (int x = 0; x < gridSettings.GridSizeX; x++)
        {
            for (int y = 0; y < gridSettings.GridSizeY; y++)

            {
                GridNode node = gridNodes[x, y];
                Gizmos.color = node.Walkable ? Color.green : Color.red;
                Gizmos.DrawWireCube(node.WorldPosition, Vector3.one * gridSettings.NodeSize * 0.9f);


            }


        }


    }

   

    public GridNode GetNode(int x, int y)
    {
        return gridNodes[x, y];
    }

    public void SetNode(int x, int y, GridNode node)
    {
        gridNodes[x, y] = node;
    }





}

