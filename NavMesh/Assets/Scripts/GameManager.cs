using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject OMWPrefab;
    public GameObject movingDetector;
    private Vector3 startPosition;
    private List<UnitRTS> selectedUnits = new List<UnitRTS>();
    public List<UnitRTS> allUnits;
    public static GameManager Instance;
    private bool dontClear=false;

    private List<List<UnitRTS>> macrosLists = new List<List<UnitRTS>>();
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        for(int i=0;i<=9;i++)macrosLists.Add(new List<UnitRTS>());
    }

    // Update is called once per frame
    void Update()
    {
        MacrosController();
        if (Input.GetMouseButtonDown(0)){
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
                Debug.Log("Aqui si que entra");
                dontClear=true;
            }
            if(!dontClear){
                ClearList();
            }
            //boton izquierdo del raton
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                if(hit.collider.gameObject.GetComponent<UnitRTS>() != null){
                Debug.Log(hit.collider.gameObject.name+" - "+hit.collider.gameObject.GetComponent<UnitRTS>());
                SelectUnit(hit.collider.gameObject.GetComponent<UnitRTS>());
                //selectedUnits.Add(hit.collider.gameObject.GetComponent<UnitRTS>());
                startPosition= hit.point;
                }
            }
            dontClear=false;
        }
        /*if (Input.GetMouseButtonUp(0)){
                        //boton izquierdo levantado
            RaycastHit hit;

            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100);
            
            Collider[] colliderArray = Physics.OverlapSphere(startPosition,hit.point.x);
            //Instantiate(Physics.OverlapSphere(startPosition,hit.point.x)[0]);
            //Debug.Log(Physics.OverlapSphere(startPosition,hit.point.x)[0].bounds.center);
            if(selectedUnits.Count != 0){
            foreach(UnitRTS unit in selectedUnits){
                unit.CanMove(false);
            }
            }
            selectedUnits.Clear();
            foreach(Collider collider in colliderArray){
                UnitRTS unit = collider.GetComponent<UnitRTS>();
                if (unit != null){
                    Debug.Log("FOUNDED "+ collider.gameObject.name);
                    selectedUnits.Add(unit);
                    unit.CanMove(true);
                }
            }
        }*/
        if (Input.GetMouseButtonDown(1)){
            if (selectedUnits.Count == 0)return;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                //Debug.Log(hit.point);
                if (movingDetector != null) Destroy(movingDetector);
                movingDetector = Instantiate(OMWPrefab, hit.point, Quaternion.identity);
            }
            foreach(UnitRTS unit in selectedUnits){
                unit.MoveCommand(hit.point);
            }
        }
    }
    public void SelectUnit(UnitRTS unit){
        selectedUnits.Add(unit);
        unit.CanMove(true);
    }

    public void ClearList(){
        foreach (UnitRTS unit in selectedUnits) unit.CanMove(false);
        selectedUnits.Clear();
    }

    void MacrosController(){
        //Debug.Log("Entra "+macrosLists.Count + " - " + (0 <= macrosLists.Count));
        // for con iteraciones por i de las listas?
        for(int i=0;i<=macrosLists.Count;i++){
            KeyCode tempKeyCode = (KeyCode)i+48;
            Debug.Log(tempKeyCode);
        if(Input.GetKeyDown(tempKeyCode)){
            Debug.Log("Entra con :" + macrosLists[i].Count + "elementos");
            if(Input.GetKey(KeyCode.LeftControl)){
                Debug.Log(macrosLists[i].Count);
                OverrideList(macrosLists[i],selectedUnits);
                Debug.Log(macrosLists[i].Count);
                return;
            }
            
            if(macrosLists[i].Count == 0) return;
            ClearList();
            foreach (UnitRTS unit in macrosLists[i]){
                SelectUnit(unit);
            }
        }
    }
        /*if(Input.GetKeyDown(KeyCode.Alpha2)){
            Debug.Log("Entra con :" + macrosLists[2].Count + "elementos");
            if(Input.GetKey(KeyCode.LeftShift)){
                Debug.Log(macrosLists[2].Count);
                OverrideList(macrosLists[2],selectedUnits);
                Debug.Log(macrosLists[2].Count);
                return;
            }
            
            if(macrosLists[2].Count == 0) return;
            ClearList();
            foreach (UnitRTS unit in macrosLists[2]){
                SelectUnit(unit);
            }
        }*/
    }
    void OverrideList(List<UnitRTS> listToOverride, List<UnitRTS> newList){
        listToOverride.Clear();
        foreach(UnitRTS unit in newList){
            listToOverride.Add(unit);
        }
    }
}
