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
        //conejo();
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
            //startPosition = new Vector2(startPosition.x-125,startPosition.y-125);
            //endPosition = new Vector2(endPosition.x+125,endPosition.y+125);
            //DrawSelection();
            if(CheckDiference(startPosition,endPosition)){
                
                startPosition = Vector2.zero;
                endPosition = Vector2.zero;
                DrawVisual();
                return;
            }
            //Debug.Log("ENTRA");
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
        //Debug.Log("Antes: "+selectionBox);
        //UnnamedMethod();
        //Debug.Log("Despues: "+selectionBox);
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
    void UnnamedMethod(){
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
    void conejo(){

int l = 500; //comiezo
int r = 1000; //limite
int suma=0;
bool breakPoint = false;
int output1=-1;
int output2=-1;
bool isSquare=false;
for(;l<=r;l++){
    Debug.Log("iteracion: " +l);
    for(int i=1;i+l<=r;i++){
        Debug.Log("segunda iteracion: "+i);
        suma = l+(l + i);
            for(int j =1;j<=r;j++){
                if(j*j == suma){
                    Debug.Log("j: "+j +", suma:" +suma);
                    isSquare=true;
                    break;
                }
            }
        if (isSquare && (suma % 1 == 0)){
            output1=l;
            output2=l+i;
            breakPoint=true;
            break;
        }

    }
    if (breakPoint){
        break;
    }
}
Debug.Log(("salida: "+output1 + output2).ToString());
    }
}
