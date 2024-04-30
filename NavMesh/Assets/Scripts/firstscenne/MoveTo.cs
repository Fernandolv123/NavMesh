using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MoveTo : MonoBehaviour
{
    NavMeshAgent agent;
    public bool moveBySphere=false;
    public Transform sphereGO;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            UnnamedMethod();
        }
        if(moveBySphere){
            if(Input.GetKeyDown(KeyCode.M)){
                MoveBySphere();
            }
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                //Debug.Log(hit.point);
                agent.destination = hit.point;
            }
        }
    }

    public void MoveBySphere(){
        agent.destination = sphereGO.position;
    }


















    void UnnamedMethod(){
        SceneManager.LoadScene("SampleScene");
    }
}