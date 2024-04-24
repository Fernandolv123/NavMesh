using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera cameraFollow;
    private Vector3 cameraFollowPosition;
    // Start is called before the first frame update
    void Start()
    {
        cameraFollowPosition = cameraFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveAmount = 100f;
        float edgeSize = 10f;
        if(Input.mousePosition.x > Screen.width - edgeSize){
            //127 y -7
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
            cameraFollow.transform.position = CameraClamped(cameraFollowPosition);
        }
        if (Input.mousePosition.x < edgeSize){
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
            cameraFollow.transform.position = CameraClamped(cameraFollowPosition);
        }
        if (Input.mousePosition.y > Screen.height - edgeSize){
            //clamp 23 y -23
            cameraFollowPosition.z -= moveAmount * Time.deltaTime;
            cameraFollow.transform.position = CameraClamped(cameraFollowPosition);
        }
        if (Input.mousePosition.y < edgeSize){
            cameraFollowPosition.z += moveAmount * Time.deltaTime;
            cameraFollow.transform.position = CameraClamped(cameraFollowPosition);
        }
        
    }
    private Vector3 CameraClamped(Vector3 newPosition){
        return new Vector3(Mathf.Clamp(newPosition.x,-7,127),newPosition.y,Mathf.Clamp(newPosition.z,-23,23));
    } 
}
