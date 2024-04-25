using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class UnitSelectionCanvas : MonoBehaviour
{
    Camera myCam;
 
    [SerializeField]
    RectTransform boxVisual;
 
    Rect selectionBox;
 
    Vector2 startPosition;
    Vector2 endPosition;
    
 
    private void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }
 
    private void Update()
    {
        // When Clicked
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
 
            // For selection the Units
            selectionBox = new Rect();
        }
 
        // When Dragging
        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }
 
        // When Releasing
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log(CheckDiference(startPosition,endPosition));
            if(CheckDiference(startPosition,endPosition)){
                startPosition = Vector2.zero;
                endPosition = Vector2.zero;
                DrawVisual();
                return;
            }
            Debug.Log("ENTRA");
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
            SelectUnits();
 

        }
    }
 
    void DrawVisual()
    {
        // Calculate the starting and ending positions of the selection box.
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;
 
        // Calculate the center of the selection box.
        Vector2 boxCenter = (boxStart + boxEnd) / 2;
 
        // Set the position of the visual selection box based on its center.
        boxVisual.position = boxCenter;
 
        // Calculate the size of the selection box in both width and height.
        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));
 
        // Set the size of the visual selection box based on its calculated size.
        boxVisual.sizeDelta = boxSize;
    }
 
    void DrawSelection()
    {
        if (Input.mousePosition.x < startPosition.x)
        {
            selectionBox.xMin = Input.mousePosition.x;
            selectionBox.xMax = startPosition.x;
        }
        else
        {
            selectionBox.xMin = startPosition.x;
            selectionBox.xMax = Input.mousePosition.x;
        }
 
 
        if (Input.mousePosition.y < startPosition.y)
        {
            selectionBox.yMin = Input.mousePosition.y;
            selectionBox.yMax = startPosition.y;
        }
        else
        {
            selectionBox.yMin = startPosition.y;
            selectionBox.yMax = Input.mousePosition.y;
        }
    }
 
    void SelectUnits()
    {
        
        GameManager.Instance.ClearList();
        foreach (var unit in GameManager.Instance.allUnits)
        {
            if (selectionBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                GameManager.Instance.SelectUnit(unit.GetComponent<UnitRTS>());
            }
        }
    }
    bool CheckDiference(Vector2 initial, Vector2 final){
        //Debug.Log(initial+" - "+final);
        //Debug.Log(Approximately(initial.x,final.x,35) + " - " + Approximately(initial.y,final.y,35));
        return Approximately(initial.x,final.x,35) && Approximately(initial.y,final.y,35);
    }

    bool Approximately(float a, float b, float difference){
        //Debug.Log(a + " >= " + (b+difference) + ": " + (a >=b +difference));
        //Debug.Log(a + " <= " + (b-difference) + ": " + (a <=b - difference));
        //Debug.Log(Mathf.Abs(b-a) < difference);
        return Mathf.Abs(b-a) < difference;

    }
}
