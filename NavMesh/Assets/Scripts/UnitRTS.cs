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
    private List<ActionRTS> queuedActions = new List<ActionRTS>();

    public bool moveActionFinished=false;
    public AudioClip audioTrain;
    private AudioSource audio;

    void Awake(){
        audio = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }
    protected virtual void Start()
    {
        Debug.Log("Soy " + gameObject.name);
    }
    public void TrainCompleted(){
        if(audioTrain!= null)audio.PlayOneShot(audioTrain);
    }


    // Update is called once per frame
    void Update()
    {
        //IsStopped solo indica si el agente puede moverse o no
        //agent.isStopped=true;
        //Debug.Log(agent.isStopped);
        //queuedActions[MoveAction()];
        if(quedActions.Count >= 1){
            Debug.Log("DIFERENCIA: "+Vector3.Distance(queuedActions[queuedActions.Count-1].GetPosition(),transform.position));
            if(Vector3.Distance(queuedActions[queuedActions.Count-1].GetPosition(),transform.position) <= 4.2F){
                // Debug.Log("ENTRA");
                moveActionFinished=true;
                agent.ResetPath();
            }
        }
        
    }
    void FixedUpdate(){

    }
    public virtual void MoveCommand(Vector3 position)
    {
        if(Input.GetKey(KeyCode.LeftShift)){
            queuedActions.Add(new ActionRTS(Order.Move,position));
            quedActions.Add(position);
            //StopCoroutine("StartPatrol");
            Debug.Log("quedActive " + queuedActive);
            if(!queuedActive)StartCoroutine("StartPatrolActionsRTS");
            return;
        }
        moveActionFinished=false;
        queuedActions.Clear();
        queuedActions.Add(new ActionRTS(Order.Move,position));
        quedActions.Clear();
        quedActions.Add(position);
        // if(!queuedActive)StartCoroutine("StartPatrolActionsRTS");
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
            //agent.destination = quedActions[i];
            //agent.ResetPath();
            
            agent.SetDestination(quedActions[i]);
            yield return new WaitForSeconds(0.01f);
            //agent.SetPath();
            //agent.nextPosition = quedActions[i++];
            while(agent.remainingDistance >= 1){
                Debug.Log(agent.remainingDistance + "<=" + 1);
                //yield return new WaitUntil(() =>navMeshStopped);
                yield return null;
                Debug.Log("SALE");
            }
            //quedActions.RemoveAt(i);
        }
        quedActions.Clear();
        queuedActive=false;
        
    }
        public IEnumerator StartPatrolActionsRTS(){
        moveActionFinished=false;
        queuedActive = true;
        for(int i=0; i<=queuedActions.Count-1;i++){
            Debug.Log(i + "<"+queuedActions.Count);
            //agent.destination = quedActions[i];
            //agent.ResetPath();
            
            queuedActions[i].ExecuteAction(agent);
            yield return new WaitForSeconds(0.01f);
            //agent.SetPath();
            //agent.nextPosition = quedActions[i++];
            while(agent.remainingDistance >= 1){
                Debug.Log(agent.remainingDistance + "<=" + 1);
                //yield return new WaitUntil(() =>navMeshStopped);
                yield return null;
                Debug.Log("SALE");
            }
            //quedActions.RemoveAt(i);
        }
        agent.ResetPath();
        moveActionFinished=true;
        queuedActions.Clear();
        queuedActive=false;
        
    }
    void OnCollisionEnter(Collision other) {
        if(GameManager.Instance.UnitInControl() < 2) return;
        if(gameObject.GetComponent<UnitRTS>().agent == null) return;
        if(other.gameObject.GetComponent<UnitRTS>() != null){
            Debug.Log("Entra");
            if(agent?.remainingDistance <=3)agent.ResetPath();

            if(other.gameObject.GetComponent<UnitRTS>().moveActionFinished)agent.ResetPath();
        }
    }
}
