using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 0.123f;
    public Vector3 offset;


    void FixedUpdate()
    {
        Vector3 desiredposition = target.position + offset;
        desiredposition.y = Mathf.Clamp(desiredposition.y,0,9999);
        //Vector3 smoothed = Vector3.Lerp(transform.position,desiredposition, speed);
        transform.position = desiredposition;

        //transform.LookAt(target);
    }
}
