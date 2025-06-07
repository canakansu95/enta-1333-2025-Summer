using System.Collections.Generic;
using UnityEngine.Diagnostics;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public List<UnitInstance> selectedUnits = new List<UnitInstance>(); // storing selected units from spawned instances
    private Vector2 dragStart, dragEnd;
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            dragStart = Input.mousePosition;
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragEnd = Input.mousePosition;
            isDragging = false;
            if (Vector2.Distance(dragStart, dragEnd) < 10f)
                SelectUnitAtMouse();
            else
                DragSelectUnits();
        }

        if (Input.GetMouseButtonDown(1)) 
        {
            MoveSelectedUnits();
        }
    }

    void SelectUnitAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // single selection method
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var unit = hit.collider.GetComponent<UnitInstance>();
            if (unit)
            {
                selectedUnits.Clear();
                selectedUnits.Add(unit);
                Debug.Log("SelectedOne");
            }
        }
    }

    void DragSelectUnits()    // calculate screen-space rectangle, loop over all UnitInstance, and add to selected units if inside
    {
        selectedUnits.Clear();
       
        foreach (var unit in FindObjectsOfType<UnitInstance>())
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);
            if (IsWithinSelectionBounds(screenPos))
                selectedUnits.Add(unit);
            Debug.Log("SelectedMultiple");
        }
    }

    bool IsWithinSelectionBounds(Vector3 screenPosition)
    {
        Rect rect = Utils.GetScreenRect(dragStart, dragEnd);
        return rect.Contains(screenPosition);
    }

    void MoveSelectedUnits() // right click method to move units
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            foreach (var unit in selectedUnits)
            {
                unit.SetDestination(hit.point);
                Debug.Log("Moved");
            }
        }
    }
}
