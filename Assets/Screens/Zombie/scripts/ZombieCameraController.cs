using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCameraController : MonoBehaviour
{
    public Camera MainCamera;
    public Vector2 CameraRange;
    public Transform Handler;
   
    void Start()
    {
        MainCamera.transform.position = Handler.position + new Vector3(0, 0, -3);
    }

    
    void LateUpdate()
    {
        Handler.position = new Vector2(Mathf.Clamp(transform.position.x, -CameraRange.x / 2, CameraRange.x / 2), Mathf.Clamp(transform.position.y, -CameraRange.y / 2, CameraRange.y / 2)) ;
        MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, Handler.position + new Vector3(0, 0, -3), 5 * Time.deltaTime);
    }
}
