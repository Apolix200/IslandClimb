using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private MonoBehaviour[] scripts;
    private movement movement;
    private float dashTime;

    public float dashSpeed;
    public float startDashTime;

    void Start()
    {
        scripts = gameObject.GetComponents<MonoBehaviour>();

        rigidBody = GetComponent<Rigidbody2D>();
        movement = GetComponent<movement>();      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (movement.getIsGrounded() || movement.getOnWall())
            {
                if (dashTime <= 0)
                {
                    dashTime = startDashTime;
                    rigidBody.velocity = Vector2.zero;
                    enableScipts(true);
                }
                else
                {
                    enableScipts(false);

                    if (transform.localScale.x > 0)
                    {
                        rigidBody.velocity = Vector2.right * dashSpeed;
                    }
                    else
                    {
                        rigidBody.velocity = Vector2.left * dashSpeed;
                    }

                    dashTime -= Time.deltaTime;
                }
            }
        }
    }

    void enableScipts(bool state)
    {
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
            {
                script.enabled = state;
            }
        }
    }
}
