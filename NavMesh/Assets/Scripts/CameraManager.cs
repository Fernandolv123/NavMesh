using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3 cameraFollowPosition;
    // Start is called before the first frame update
    void Start()
    {
        cameraFollowPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //esto esta acumulando velocidad, hay que replaantearse la posicion del clamp
        float moveAmount = 100f;
        float edgeSize = 10f;
        if(Input.mousePosition.x > Screen.width - edgeSize){
            cameraFollowPosition.x -= moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.x < edgeSize){
            cameraFollowPosition.x += moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.y > Screen.height - edgeSize){
            cameraFollowPosition.z -= moveAmount * Time.deltaTime;
        }
        if (Input.mousePosition.y < edgeSize){
            cameraFollowPosition.z += moveAmount * Time.deltaTime;
        }
        cameraFollowPosition = new Vector3(Mathf.Clamp(cameraFollowPosition.x,-7,127),cameraFollowPosition.y,Mathf.Clamp(cameraFollowPosition.z,-23,23));
        transform.position = cameraFollowPosition;
        //Debug.Log(cameraFollowPosition);
    }
}
