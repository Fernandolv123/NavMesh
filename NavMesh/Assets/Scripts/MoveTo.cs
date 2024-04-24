using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject OMWprefab;
    private GameObject movingGO;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                //Debug.Log(hit.point);
                if (movingGO != null) Destroy(movingGO);
                movingGO =Instantiate(OMWprefab,hit.point,Quaternion.identity);
                agent.destination = hit.point;
            }
        }
    }
}