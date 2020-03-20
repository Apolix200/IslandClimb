using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    MonoBehaviour[] scripts;
    DistanceJoint2D joint;
    GameObject movementScript;
    Vector3 targetPos;
    RaycastHit2D hit;

    public float distance = 10f;
    public LayerMask mask;

    void Start()
    {
        scripts = gameObject.GetComponents<MonoBehaviour>();

        joint = GetComponent<DistanceJoint2D> ();
        joint.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown (KeyCode.E))
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, distance, mask);

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                enableScipts(false);
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.distance = Vector2.Distance(transform.position, hit.point);
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            enableScipts(true);
            joint.enabled = false;
        }
    }

    void enableScipts(bool state)
    {
        foreach (MonoBehaviour script in scripts)
        {
            if(script != this)
            {
                script.enabled = state;
            }      
        }
    }
}
