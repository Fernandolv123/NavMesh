using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitRTS : MonoBehaviour
{
    [SerializeField]private GameObject selectedUnit;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {

    }
    public void MoveCommand(Vector3 position)
    {
        agent.destination = position;
    }
    public void CanMove(bool status){
        selectedUnit.SetActive(status);
    }
}
