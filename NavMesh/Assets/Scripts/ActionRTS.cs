using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

class ActionRTS
{
    private Order orderToDo;
    private Vector3 positionToMove;
    private GameObject buildToConstruct;
    public ActionRTS(Order order,Vector3 position){
        orderToDo = order;
        positionToMove = position;
    }
    public ActionRTS(Order order,Vector3 position,GameObject build){
        orderToDo = order;
        positionToMove = position;
        buildToConstruct = build;
    }
    public void ExecuteAction(NavMeshAgent agent){
        switch(orderToDo){
            case Order.Move:
                agent.destination = positionToMove;
                break;
            case Order.Build:
                agent.destination = positionToMove;
                break;
        }
    }
    public Vector3 GetPosition(){
        return positionToMove;
    }
}
enum Order{
    Move,
    Build
};
