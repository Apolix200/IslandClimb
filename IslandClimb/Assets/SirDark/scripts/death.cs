using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour
{
    private Vector3 rp = new Vector3(0f,-3.4f,0f);
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Spike"){
            gameObject.transform.position = rp;
        }   
        if(other.gameObject.tag == "CheckPoint"){
            rp = other.gameObject.transform.position;
            Destroy(other.gameObject);
        }
    }
}
