
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  
    [Header("Camera Follow")]
    public Transform target = null;
    public float smoothSpeed = 0.125f;

    [Header("Camera Limits")]
    public float leftLimit;
    public float rightLimit;
    public float topLimit;
    public float bottomLimit;

    public void FixedUpdate()
    {

        //Code to have the camera follow the player, uses Lerp to smooth the camera so that it does not move around too much in certain situations
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        

        //Clamps the camera to the limits set: 
        transform.position = new Vector3
        (
            smoothedPosition.x,
            smoothedPosition.y,
            -10
        );
        

    }
    
}