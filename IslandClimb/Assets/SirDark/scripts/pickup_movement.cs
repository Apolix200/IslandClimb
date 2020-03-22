using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_movement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        /*Debug.Log(pos.y);
        Deug.Log(pos.y + "+" + Mathf.Sin(Time.fixedTime) + "=" + (pos.y+Mathf.Sin(Time.fixedTime)));*/
        pos.y += Mathf.Sin(Time.fixedTime*2) / 1000;
        
        
        gameObject.transform.position = pos;
    }
}
