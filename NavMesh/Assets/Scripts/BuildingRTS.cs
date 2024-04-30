using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRTS : UnitRTS
{
    [SerializeField]
    private Vector3 queuePoint;

    public List<GameObject> unitPrefab;
    private float timer;

    private UnitRTS unitTrained;
    private bool alreadyUnderUse;
    private const int MAX_QUEUE_LIMIT=4;
    private int trainingQueue;

    // Start is called before the first frame update
    protected override void Start()
    {
        trainingQueue=0;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(MAX_QUEUE_LIMIT >= trainingQueue)StartCoroutine("Train");

            Debug.Log(MAX_QUEUE_LIMIT+ " <= " +trainingQueue);
        }
    }

    public override void MoveCommand(Vector3 position)
    {
        queuePoint = position;
    }

    public IEnumerator Train()
    {
        trainingQueue++;
        while(alreadyUnderUse){
            yield return new WaitForEndOfFrame();
        }
        alreadyUnderUse=true;
        timer = 0;
        while (timer <= 3){
            timer++;
            yield return new WaitForSeconds(1f);
        }
            unitTrained = Instantiate(unitPrefab[0],transform.position, Quaternion.identity).GetComponent<UnitRTS>();
            GameManager.Instance.allUnits.Add(unitTrained);
            Invoke("MoveUnit",0.05f);
            alreadyUnderUse=false;
            trainingQueue--;
            
    }

    public void MoveUnit(){
        unitTrained.MoveCommand(queuePoint);
    }
}
