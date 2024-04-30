using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitRTS : MonoBehaviour
{
    [SerializeField]private GameObject selectedUnit;
    private NavMeshAgent agent;

    private List<Vector3> quedActions = new List<Vector3>();
    private bool queuedActive=false;


    private Vector3 previousPosition;
    private bool navMeshStopped=false;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        previousPosition=transform.position;
        Debug.Log("Soy " + gameObject.name);
    }


    // Update is called once per frame
    void Update()
    {
        //IsStopped solo indica si el agente puede moverse o no
        //agent.isStopped=true;
        //Debug.Log(agent.isStopped);
        Debug.Log(navMeshStopped);
        if(previousPosition == transform.position){
            navMeshStopped = true;
        } else {
            navMeshStopped = false;
            previousPosition = transform.position;
        }
        
    }
    void FixedUpdate(){

    }
    public virtual void MoveCommand(Vector3 position)
    {
        if(Input.GetKey(KeyCode.LeftShift)){
            quedActions.Add(position);
            //StopCoroutine("StartPatrol");
            if(!queuedActive)StartCoroutine("StartPatrol");
            return;
        }
        quedActions.Clear();
        quedActions.Add(position);
        agent.autoBraking=true;
        agent.destination = position;
    }
    public void CanMove(bool status){
        selectedUnit.SetActive(status);
    }

    public IEnumerator StartPatrol(){
        queuedActive = true;
        for(int i=0; i<=quedActions.Count-1;i++){
            Debug.Log(i + "<"+quedActions.Count);
            agent.destination = quedActions[i];
            //agent.nextPosition = quedActions[i++];
            while(agent.remainingDistance >= 1){
                yield return new WaitUntil(() =>navMeshStopped);
                Debug.Log("SALE");
            }
            //quedActions.RemoveAt(i);
        }
        quedActions.Clear();
        queuedActive=false;
        
    }
}
