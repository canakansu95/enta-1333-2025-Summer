using System;
using UnityEngine;


public class UnitInstance : UnitBase   // represents a spawned, active unit in the scene 
{
    [Header("Visuals & FX")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject skinRoot;
    [SerializeField] private LineRenderer pathLine;
    // [SerializeField] private ParticleSystem hurtFX;


    public void Initialize(AStarPathfinder assignedPathfinder, Material teamMaterial) // assign material to each unit belonging to different armies
    {
        pathfinder = assignedPathfinder;

        foreach (var renderer in skinRoot.GetComponentsInChildren<Renderer>())
        {
          
            var mats = renderer.materials;
            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = teamMaterial;
            }
            renderer.materials = mats;
        }
    }



    public void SetDestination(Vector3 position)   // set a movement target using a world position
    {
        movementTarget = position;
        path = pathfinder.FindPath(transform.position, position);
        pathIndex = 0;
        moving = path != null && path.Count > 1;
        DrawPathLine();
    }

   
    public void SetDestination(GridNode node)   // set a movement target using a grid node
    {
        SetDestination(node.WorldPosition);
    }

   
    public override void MoveTo(GridNode node)
    {
        SetDestination(node);
    }

    void Update()
    {
        if (moving && path != null && pathIndex < path.Count)
        {
            Vector3 nextPoint = path[pathIndex].WorldPosition;
            float step = 5f * Time.deltaTime; // adjust movement speed here
            transform.position = Vector3.MoveTowards(transform.position, nextPoint, step);

            if (Vector3.Distance(transform.position, nextPoint) < 0.05f)
            {
                pathIndex++;
                if (pathIndex >= path.Count)
                {
                    moving = false;
                    // later I will put idle anim
                    // if (animator) animator.SetBool("Moving", false);
                }




            }
        }
    }

    private void DrawPathLine() // drawing a line to show path
    {
        if (pathLine == null || path == null || path.Count == 0)
        {
            if (pathLine != null) pathLine.positionCount = 0;
            return;
        }
        pathLine.positionCount = path.Count;
        for (int i = 0; i < path.Count; i++)
        {
            pathLine.SetPosition(i, path[i].WorldPosition + Vector3.up * 0.1f);
        }
        pathLine.startColor = Color.yellow;
        pathLine.endColor = Color.red;
    }
}