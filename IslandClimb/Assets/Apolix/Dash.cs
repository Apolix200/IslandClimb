using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Animator animator;
    private MonoBehaviour[] scripts;
    private Rigidbody2D rigidBody;
    private movement movement;
    private bool canDash;
    private float dashTime;
    private int direction;

    public GameObject dashParticle;
    public float dashSpeed = 20f;
    public float startDashTime = 5f;

    void Start()
    {
        scripts = gameObject.GetComponents<MonoBehaviour>();
        rigidBody = GetComponent<Rigidbody2D>();
        movement = GetComponent<movement>();
        animator = GetComponent<Animator>();

        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && canDash)
            { 
                enableScipts(false);
                canDash = false;

                Instantiate(dashParticle, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                animator.SetBool("dash", true);

                if (transform.localScale.x > 0)
                {
                    direction = 1;
                }
                else
                {
                    direction = 2;
                }
            }
        }
        else
        {
            Instantiate(dashParticle, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);

            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rigidBody.velocity = Vector2.zero;
                enableScipts(true);
                animator.SetBool("dash", false);
            }
            else
            {
                dashTime -= Time.deltaTime;

                if(direction == 1)
                {
                    rigidBody.velocity = Vector2.right * dashSpeed;
                }
                else if (direction == 2)
                {
                    rigidBody.velocity = Vector2.left * dashSpeed;
                }
            }
        }

        if(movement.getIsGrounded() || movement.getOnWall())
        {
            canDash = true;
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
