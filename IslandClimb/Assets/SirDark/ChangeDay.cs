using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDay : MonoBehaviour
{
    public GameObject skytop;
    public Sprite night;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            skytop.GetComponent<SpriteRenderer>().sprite = night;


            Destroy(gameObject);
        }
    }
}
